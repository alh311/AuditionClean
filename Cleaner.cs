using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AuditionClean
{
    public class Cleaner
    {
        public Cleaner() { }

        public void Run()
        {
            var currentDirectory = GetCurrentDirectory();
            var searchableString = GetSearchableString(currentDirectory);
            var recordedFiles = GetRecordedFiles(currentDirectory);

            var keepFiles = new List<FileInfo>();
            var deleteErrors = new List<string>();
            var deletedFilesCount = 0;
            long deletedFilesSize = 0;

            // Loop through all files and see if they exist in the searchable string.
            foreach (var file in recordedFiles)
            {
                // Get the file name without the extension but with "/" and "." start and end characters.
                // Doing this to keep .pkf files from getting deleted if their respective audio file is retained.
                var formattedFileName = $"/{Path.GetFileNameWithoutExtension(file.Name)}.";

                if (searchableString.Contains(formattedFileName))
                {
                    keepFiles.Add(file);
                }
                else
                {
                    try
                    {
                        deletedFilesSize += GetFileSize(file);
                        file.Delete();
                        deletedFilesCount++;
                    }
                    catch (Exception e)
                    {
                        deleteErrors.Add(e.Message);
                    }
                }
            }

            Console.WriteLine("\n\nKEPT FILES:");
            keepFiles.ForEach(file => Console.WriteLine(file.Name));

            if (deleteErrors.Any())
            {
                Console.WriteLine("\n\nDELETE ERRORS:");
                deleteErrors.ForEach(error => Console.WriteLine(error));
            }

            // Convert bytes to Mbs, then format to 2 decimals
            var deletedMbs = (deletedFilesSize / (1024F * 1024F)).ToString("N2");
            Console.WriteLine($"\n\nDeleted {deletedFilesCount} files, which is {deletedMbs} Mb.");
        }

        /// <summary>
        /// Gets the directory from where the app was executed.
        /// </summary>
        /// <returns>The current directory info.</returns>
        private DirectoryInfo GetCurrentDirectory()
        {
            var currentPath = Environment.CurrentDirectory;
            var currentDirectory = new DirectoryInfo(currentPath);

            if (currentDirectory.Exists)
            {
                Console.WriteLine($"Current Directory: {currentDirectory.FullName}");
                return currentDirectory;
            }
            else
            {
                throw new Exception($"Error loading directory: {currentPath}");
            }
        }

        /// <summary>
        /// Gets a string containing all the file tags which were found in the Audition session file.
        /// </summary>
        /// <param name="currentDirectory"></param>
        /// <returns></returns>
        private string GetSearchableString(DirectoryInfo currentDirectory)
        {
            FileInfo[] sessionFiles;
            try
            {
                sessionFiles = currentDirectory.GetFiles("*.sesx");
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error getting the session files: {e.Message}");
            }

            if (sessionFiles.Length == 0)
            {
                throw new Exception("There were no session files in the directory.  There should be exactly one session file.");
            }
            else if (sessionFiles.Length > 1)
            {
                throw new Exception("There were too many session files in the directory.  There should be only one session file.");
            }

            var sessionFileName = sessionFiles.First().Name;

            // Match only the file tags in the session file.
            var regex = new Regex(@"\<file .+/\>");

            // Get list of files in the .sesx
            var matchingLines = File.ReadAllLines(sessionFileName)
                .Where(line => regex.IsMatch(line))
                .Select(line => line.Trim());

            // Combile all the file tags into a single searchable string.
            return string.Join(string.Empty, matchingLines);
        }

        /// <summary>
        /// Gets an array of all the files in the _Recorded directory.
        /// </summary>
        /// <param name="currentDirectory"></param>
        /// <returns></returns>
        private FileInfo[] GetRecordedFiles(DirectoryInfo currentDirectory)
        {
            try
            {
                var recordedDirectory = currentDirectory.GetDirectories("*_Recorded").FirstOrDefault();
                var files = recordedDirectory.GetFiles();
                return files;
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error getting the recorded files: {e.Message}");
            }
        }

        /// <summary>
        /// Gets the file size, swallowing any exceptions because file size isn't THAT important to have.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private long GetFileSize(FileInfo file)
        {
            try
            {
                return file.Length;
            }
            catch
            {
                return 0;
            }
        }
    }
}

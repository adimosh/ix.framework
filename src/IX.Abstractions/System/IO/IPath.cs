﻿// <copyright file="IPath.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.System.IO
{
    /// <summary>
    /// Abstracts the <see cref="T:System.IO.Path"/> class' static methods into a mock-able interface.
    /// </summary>
    public interface IPath
    {
        /// <summary>
        /// Gets a platform-specific alternate character used to separate directory levels in a path string that reflects a hierarchical file system organization.
        /// </summary>
        char AltDirectorySeparatorChar { get; }

        /// <summary>
        /// Gets a platform-specific character used to separate directory levels in a path string that reflects a hierarchical file system organization.
        /// </summary>
        char DirectorySeparatorChar { get; }

        /// <summary>
        /// Gets a platform-specific separator character used to separate path strings in environment variables.
        /// </summary>
        char PathSeparator { get; }

        /// <summary>
        /// Gets a platform-specific volume separator character.
        /// </summary>
        char VolumeSeparatorChar { get; }

        /// <summary>
        /// Changes the extension of a path string.
        /// </summary>
        /// <param name="path">The path information to modify. The path cannot contain any of the characters defined in <see cref="GetInvalidPathChars"/>.</param>
        /// <param name="extension">The new extension (with or without a leading period). Specify <c>null</c> (<c>Nothing</c> in Visual Basic) to remove an existing extension from path.</param>
        /// <returns>The modified path information.</returns>
        string ChangeExtension(string path, string extension);

        /// <summary>
        /// Combines an array of strings into a path.
        /// </summary>
        /// <param name="paths">An array of parts of the path.</param>
        /// <returns>The combined paths.</returns>
        string Combine(params string[] paths);

        /// <summary>
        /// Returns the directory information for the specified path string.
        /// </summary>
        /// <param name="path">The path of a file or directory.</param>
        /// <returns>Directory information for path, or <c>null</c> if path denotes a root directory or is <c>null</c>. Returns <see cref="string.Empty"/> if path does not contain directory information.</returns>
        string GetDirectoryName(string path);

        /// <summary>
        /// Returns the extension of the specified path string.
        /// </summary>
        /// <param name="path">The path string from which to get the extension.</param>
        /// <returns>The extension of the specified path (including the period &quot;.&quot;), or <c>null</c>, or <see cref="string.Empty"/>. If path is <c>null</c>, the method returns <c>null</c>. If path does not have extension information, the method returns <see cref="string.Empty"/>.</returns>
        string GetExtension(string path);

        /// <summary>
        /// Returns the file name and extension of the specified path string.
        /// </summary>
        /// <param name="path">The path string from which to obtain the file name and extension.</param>
        /// <returns>The characters after the last directory character in path. If the last character of path is a directory or volume separator character, this method returns <see cref="string.Empty"/>. If path is <c>null</c>, this method returns <c>null</c>.</returns>
        /// <exception cref="global::System.ArgumentException"><paramref name="path"/> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        string GetFileName(string path);

        /// <summary>
        /// Returns the file name of the specified path string without the extension.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>The string returned by <see cref="GetFileName(string)"/> , minus the last period (.) and all characters following it.</returns>
        string GetFileNameWithoutExtension(string path);

        /// <summary>
        /// Returns the absolute path for the specified path string.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain absolute path information.</param>
        /// <returns>The fully qualified location of path, such as &quot;C:\MyFile.txt&quot;.</returns>
        string GetFullPath(string path);

        /// <summary>
        /// Gets an array containing the characters that are not allowed in file names.
        /// </summary>
        /// <returns>An array containing the characters that are not allowed in file names.</returns>
        char[] GetInvalidFileNameChars();

        /// <summary>
        /// Gets an array containing the characters that are not allowed in path names.
        /// </summary>
        /// <returns>An array containing the characters that are not allowed in path names.</returns>
        char[] GetInvalidPathChars();

        /// <summary>
        /// Gets the root directory information of the specified path.
        /// </summary>
        /// <param name="path">The path from which to obtain root directory information.</param>
        /// <returns>The root directory of path, such as &quot;C:\&quot;, or <c>null</c> if <paramref name="path"/> is <c>null</c>, or an empty string if <paramref name="path"/> does not contain root directory information.</returns>
        string GetPathRoot(string path);

        /// <summary>
        /// Returns a random folder name or file name.
        /// </summary>
        /// <returns>A random folder name or file name.</returns>
        string GetRandomFileName();

        /// <summary>
        /// Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
        /// </summary>
        /// <returns>The full path of the temporary file.</returns>
        string GetTempFileName();

        /// <summary>
        /// Returns the path of the current user's temporary folder.
        /// </summary>
        /// <returns>The path to the temporary folder, ending with a backslash.</returns>
        string GetTempPath();

        /// <summary>
        /// Determines whether a path includes a file name extension.
        /// </summary>
        /// <param name="path">The path to search for an extension.</param>
        /// <returns><c>true</c> if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the path include a period (.) followed by one or more characters; otherwise, <c>false</c>.</returns>
        bool HasExtension(string path);

        /// <summary>
        /// Gets a value indicating whether the specified path string contains a root.
        /// </summary>
        /// <param name="path">The path to test.</param>
        /// <returns><c>true</c> if path contains a root; otherwise, <c>false</c>.</returns>
        bool IsPathRooted(string path);
    }
}
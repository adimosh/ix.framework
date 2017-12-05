﻿// <copyright file="SaveWhenDisposingMemoryStream.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.IO;

namespace IX.Abstractions.Moq
{
    /// <summary>
    /// A memory stream that saves when it disposes.
    /// </summary>
    public class SaveWhenDisposingMemoryStream : MemoryStream
    {
        private readonly Action<byte[]> saveFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveWhenDisposingMemoryStream"/> class.
        /// </summary>
        /// <param name="saveFile">The file save action that should be invoked when this instance is correctly disposed.</param>
        /// <exception cref="T:System.ArgumentNullException">Occurs when the <paramref name="saveFile"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public SaveWhenDisposingMemoryStream(Action<byte[]> saveFile)
            : base()
        {
            this.saveFile = saveFile ?? throw new ArgumentNullException(nameof(saveFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveWhenDisposingMemoryStream"/> class.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream.</param>
        /// <param name="saveFile">The file save action that should be invoked when this instance is correctly disposed.</param>
        /// <exception cref="T:System.ArgumentNullException">Occurs when the <paramref name="saveFile"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public SaveWhenDisposingMemoryStream(byte[] buffer, Action<byte[]> saveFile)
            : base(buffer)
        {
            this.saveFile = saveFile ?? throw new ArgumentNullException(nameof(saveFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveWhenDisposingMemoryStream"/> class.
        /// </summary>
        /// <param name="capacity">The initial size of the internal array in bytes.</param>
        /// <param name="saveFile">The file save action that should be invoked when this instance is correctly disposed.</param>
        /// <exception cref="T:System.ArgumentNullException">Occurs when the <paramref name="saveFile"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public SaveWhenDisposingMemoryStream(int capacity, Action<byte[]> saveFile)
            : base(capacity)
        {
            this.saveFile = saveFile ?? throw new ArgumentNullException(nameof(saveFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveWhenDisposingMemoryStream"/> class.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream.</param>
        /// <param name="writable">The setting of the <see cref="M:System.IO.MemoryStream.CanWrite"/> property, which determines whether the stream supports writing.</param>
        /// <param name="saveFile">The file save action that should be invoked when this instance is correctly disposed.</param>
        /// <exception cref="T:System.ArgumentNullException">Occurs when the <paramref name="saveFile"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public SaveWhenDisposingMemoryStream(byte[] buffer, bool writable, Action<byte[]> saveFile)
            : base(buffer, writable)
        {
            this.saveFile = saveFile ?? throw new ArgumentNullException(nameof(saveFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveWhenDisposingMemoryStream"/> class.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream.</param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes.</param>
        /// <param name="saveFile">The file save action that should be invoked when this instance is correctly disposed.</param>
        /// <exception cref="T:System.ArgumentNullException">Occurs when the <paramref name="saveFile"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public SaveWhenDisposingMemoryStream(byte[] buffer, int index, int count, Action<byte[]> saveFile)
            : base(buffer, index, count)
        {
            this.saveFile = saveFile ?? throw new ArgumentNullException(nameof(saveFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveWhenDisposingMemoryStream"/> class.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream.</param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes.</param>
        /// <param name="writable">The setting of the <see cref="M:System.IO.MemoryStream.CanWrite"/> property, which determines whether the stream supports writing.</param>
        /// <param name="saveFile">The file save action that should be invoked when this instance is correctly disposed.</param>
        /// <exception cref="T:System.ArgumentNullException">Occurs when the <paramref name="saveFile"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public SaveWhenDisposingMemoryStream(byte[] buffer, int index, int count, bool writable, Action<byte[]> saveFile)
            : base(buffer, index, count, writable)
        {
            this.saveFile = saveFile ?? throw new ArgumentNullException(nameof(saveFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveWhenDisposingMemoryStream"/> class.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream.</param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes.</param>
        /// <param name="writable">The setting of the <see cref="M:System.IO.MemoryStream.CanWrite"/> property, which determines whether the stream supports writing.</param>
        /// <param name="publiclyVisible">
        /// <c>true</c> to enable <see cref="M:System.IO.MemoryStream.GetBuffer"/>, which returns the unsigned byte array from which the stream was created; otherwise, <c>false</c>.
        /// </param>
        /// <param name="saveFile">The file save action that should be invoked when this instance is correctly disposed.</param>
        /// <exception cref="T:System.ArgumentNullException">Occurs when the <paramref name="saveFile"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public SaveWhenDisposingMemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible, Action<byte[]> saveFile)
            : base(buffer, index, count, writable, publiclyVisible)
        {
            this.saveFile = saveFile ?? throw new ArgumentNullException(nameof(saveFile));
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.IO.MemoryStream" /> class and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    this.saveFile(this.ToArray());
                }
                catch
                {
                }
            }

            base.Dispose(disposing);
        }
    }
}
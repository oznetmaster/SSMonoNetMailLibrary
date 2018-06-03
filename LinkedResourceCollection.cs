//
// System.Net.Mail.LinkedResourceCollection.cs
//
// Author:
//	John Luke (john.luke@gmail.com)
// Neil Colvin
//
// Copyright (C) John Luke, 2005
// Copyright (C) Nivloc Enterprises Ltd, 2018
//

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Collections.ObjectModel;
#if SSHARP
using Crestron.SimplSharp.CrestronIO;
using SSMono.Net.Mime;
using GC = Crestron.SimplSharp.CrestronEnvironment.GC;
#else
using System.IO;
using System.Net.Mime;
#endif

#if SSHARP
namespace SSMono.Net.Mail
#else
namespace System.Net.Mail
#endif
	{
	public sealed class LinkedResourceCollection : Collection<LinkedResource>, IDisposable
		{
		#region Fields

		#endregion // Fields

		#region Constructors

		internal LinkedResourceCollection ()
			{
			}

		#endregion // Constructors

		#region Properties

		#endregion // Properties

		#region Methods

		public void Dispose ()
			{
			Dispose (true);
			GC.SuppressFinalize (this);
			}

		private void Dispose (bool disposing)
			{
			}

		protected override void ClearItems ()
			{
			base.ClearItems ();
			}

		protected override void InsertItem (int index, LinkedResource item)
			{
			base.InsertItem (index, item);
			}

		protected override void RemoveItem (int index)
			{
			base.RemoveItem (index);
			}

		protected override void SetItem (int index, LinkedResource item)
			{
			base.SetItem (index, item);
			}

		#endregion // Methods
		}
	}

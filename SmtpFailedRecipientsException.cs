//
// System.Net.Mail.SmtpFailedRecipientsException.cs
//
// Author:
//	Tim Coleman (tim@timcoleman.com)
// Neil Colvin
//
// Copyright (C) Tim Coleman, 2004
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
using System.Collections;
using System.Runtime.Serialization;

#if SSHARP
namespace SSMono.Net.Mail
#else
namespace System.Net.Mail
#endif
	{
	[Serializable]
	public class SmtpFailedRecipientsException : SmtpFailedRecipientException
#if !NETCF
		, ISerializable
#endif
		{
		#region Fields

		private SmtpFailedRecipientException[] innerExceptions;

		#endregion // Fields

		#region Constructors

		public SmtpFailedRecipientsException ()
			{
			}

		public SmtpFailedRecipientsException (string message)
			: base (message)
			{
			}

		public SmtpFailedRecipientsException (string message, Exception innerException)
			: base (message, innerException)
			{
			}

		public SmtpFailedRecipientsException (string message, SmtpFailedRecipientException[] innerExceptions)
			: base (message)
			{
			this.innerExceptions = innerExceptions;
			}

#if !NETCF
		protected SmtpFailedRecipientsException (SerializationInfo info, StreamingContext context)
			: base (info, context)
			{
			if (info == null)
				throw new ArgumentNullException ("info");
			innerExceptions = (SmtpFailedRecipientException[])info.GetValue ("innerExceptions", typeof(SmtpFailedRecipientException[]));
			}
#endif

		#endregion

		#region Properties

		public SmtpFailedRecipientException[] InnerExceptions
			{
			get { return innerExceptions; }
			}

		#endregion // Properties

		#region Methods

#if !NETCF
		public override void GetObjectData (SerializationInfo info, StreamingContext context)
			{
			if (info == null)
				throw new ArgumentNullException ("info");
			base.GetObjectData (info, context);
			info.AddValue ("innerExceptions", innerExceptions);
			}

		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
			{
			GetObjectData (info, context);
			}
#endif
		#endregion // Methods
		}
	}

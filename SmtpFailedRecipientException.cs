//
// System.Net.Mail.SmtpFailedRecipientException.cs
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
using System.Collections;
using System.Runtime.Serialization;

#if SSHARP
namespace SSMono.Net.Mail
#else
namespace System.Net.Mail
#endif
	{
	[Serializable]
	public class SmtpFailedRecipientException : SmtpException
#if !NETCF
		, ISerializable
#endif
		{
		#region Fields

		private string failedRecipient;

		#endregion // Fields

		#region Constructors

		public SmtpFailedRecipientException ()
			{
			}

		public SmtpFailedRecipientException (string message)
			: base (message)
			{
			}

#if !NETCF
		protected SmtpFailedRecipientException (SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base (serializationInfo, streamingContext)
			{
			if (serializationInfo == null)
				throw new ArgumentNullException ("serializationInfo");
			failedRecipient = serializationInfo.GetString ("failedRecipient");
			}
#endif

		public SmtpFailedRecipientException (SmtpStatusCode statusCode, string failedRecipient)
			: base (statusCode)
			{
			this.failedRecipient = failedRecipient;
			}

		public SmtpFailedRecipientException (string message, Exception innerException)
			: base (message, innerException)
			{
			}

		public SmtpFailedRecipientException (string message, string failedRecipient, Exception innerException)
			: base (message, innerException)
			{
			this.failedRecipient = failedRecipient;
			}

		public SmtpFailedRecipientException (SmtpStatusCode statusCode, string failedRecipient, string serverResponse)
			: base (statusCode, serverResponse)
			{
			this.failedRecipient = failedRecipient;
			}

		#endregion // Constructors

		#region Properties

		public string FailedRecipient
			{
			get { return failedRecipient; }
			}

		#endregion // Properties

		#region Methods

#if !NETCF
		public override void GetObjectData (SerializationInfo serializationInfo, StreamingContext streamingContext)
			{
			if (serializationInfo == null)
				throw new ArgumentNullException ("serializationInfo");
			base.GetObjectData (serializationInfo, streamingContext);
			serializationInfo.AddValue ("failedRecipient", failedRecipient);
			}

		void ISerializable.GetObjectData (SerializationInfo serializationInfo, StreamingContext streamingContext)
			{
			GetObjectData (serializationInfo, streamingContext);
			}
#endif
		#endregion // Methods
		}
	}

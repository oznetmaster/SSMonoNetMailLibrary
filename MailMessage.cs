//
// System.Net.Mail.MailMessage.cs
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
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
#if SSHARP
using SSMono.Net.Mime;
using GC = Crestron.SimplSharp.CrestronEnvironment.GC;
#else
using System.Net.Mime;
#endif

#if SSHARP
namespace SSMono.Net.Mail
#else
namespace System.Net.Mail
#endif
	{
	public class MailMessage : IDisposable
		{
		#region Fields

		private AlternateViewCollection alternateViews;
		private AttachmentCollection attachments;
		private MailAddressCollection bcc;
		private MailAddressCollection replyTo;
		private string body;
		private MailPriority priority;
		private MailAddress sender;
		private DeliveryNotificationOptions deliveryNotificationOptions;
		private MailAddressCollection cc;
		private MailAddress from;
		private NameValueCollection headers;
		private MailAddressCollection to;
		private string subject;
		private Encoding subjectEncoding, bodyEncoding, headersEncoding = Encoding.UTF8;
		private bool isHtml;

		#endregion // Fields

		#region Constructors

		public MailMessage ()
			{
			this.to = new MailAddressCollection ();

			alternateViews = new AlternateViewCollection ();
			attachments = new AttachmentCollection ();
			bcc = new MailAddressCollection ();
			cc = new MailAddressCollection ();
			replyTo = new MailAddressCollection ();
			headers = new NameValueCollection ();

			headers.Add ("MIME-Version", "1.0");
			}

		// FIXME: should it throw a FormatException if the addresses are wrong? 
		// (How is it possible to instantiate such a malformed MailAddress?)
		public MailMessage (MailAddress from, MailAddress to)
			: this ()
			{
			if (from == null || to == null)
				throw new ArgumentNullException ();

			From = from;

			this.to.Add (to);
			}

		public MailMessage (string from, string to)
			: this ()
			{
			if (from == null || from == String.Empty)
				throw new ArgumentNullException ("from");
			if (to == null || to == String.Empty)
				throw new ArgumentNullException ("to");

			this.from = new MailAddress (from);
			foreach (string recipient in to.Split (new char[] {','}))
				this.to.Add (new MailAddress (recipient.Trim ()));
			}

		public MailMessage (string from, string to, string subject, string body)
			: this ()
			{
			if (from == null || from == String.Empty)
				throw new ArgumentNullException ("from");
			if (to == null || to == String.Empty)
				throw new ArgumentNullException ("to");

			this.from = new MailAddress (from);
			foreach (string recipient in to.Split (new char[] {','}))
				this.to.Add (new MailAddress (recipient.Trim ()));

			Body = body;
			Subject = subject;
			}

		#endregion // Constructors

		#region Properties

		public AlternateViewCollection AlternateViews
			{
			get { return alternateViews; }
			}

		public AttachmentCollection Attachments
			{
			get { return attachments; }
			}

		public MailAddressCollection Bcc
			{
			get { return bcc; }
			}

		public string Body
			{
			get { return body; }
			set
				{
				// autodetect suitable body encoding (ASCII or UTF-8), if it is not initialized yet.
				if (value != null && bodyEncoding == null)
					bodyEncoding = GuessEncoding (value) ?? Encoding.ASCII;
				body = value;
				}
			}

		internal ContentType BodyContentType
			{
			get
				{
				ContentType ct = new ContentType (isHtml ? "text/html" : "text/plain");
#if NETCF
				ct.CharSet = (BodyEncoding ?? Encoding.ASCII).WebName;
#else
				ct.CharSet = (BodyEncoding ?? Encoding.ASCII).HeaderName;
#endif
				return ct;
				}
			}

		internal TransferEncoding ContentTransferEncoding
			{
			get { return ContentType.GuessTransferEncoding (BodyEncoding); }
			}

		public Encoding BodyEncoding
			{
			get { return bodyEncoding; }
			set { bodyEncoding = value; }
			}

		public MailAddressCollection CC
			{
			get { return cc; }
			}

		public DeliveryNotificationOptions DeliveryNotificationOptions
			{
			get { return deliveryNotificationOptions; }
			set { deliveryNotificationOptions = value; }
			}

		public MailAddress From
			{
			get { return from; }
			set { from = value; }
			}

		public NameValueCollection Headers
			{
			get { return headers; }
			}

		public bool IsBodyHtml
			{
			get { return isHtml; }
			set { isHtml = value; }
			}

		public MailPriority Priority
			{
			get { return priority; }
			set { priority = value; }
			}

#if NET_4_0
		public
#else
		internal
#endif
			Encoding HeadersEncoding
			{
			get { return headersEncoding; }
			set { headersEncoding = value; }
			}

#if NET_4_0
		public
#else
		internal
#endif
			MailAddressCollection ReplyToList
			{
			get { return replyTo; }
			}

#if NET_4_0
		[Obsolete ("Use ReplyToList instead")]
#endif

		public MailAddress ReplyTo
			{
			get
				{
				if (replyTo.Count == 0)
					return null;
				return replyTo[0];
				}
			set
				{
				replyTo.Clear ();
				replyTo.Add (value);
				}
			}

		public MailAddress Sender
			{
			get { return sender; }
			set { sender = value; }
			}

		public string Subject
			{
			get { return subject; }
			set
				{
				if (value != null && subjectEncoding == null)
					subjectEncoding = GuessEncoding (value);
				subject = value;
				}
			}

		public Encoding SubjectEncoding
			{
			get { return subjectEncoding; }
			set { subjectEncoding = value; }
			}

		public MailAddressCollection To
			{
			get { return to; }
			}

		#endregion // Properties

		#region Methods

		public void Dispose ()
			{
			Dispose (true);
			GC.SuppressFinalize (this);
			}

		protected virtual void Dispose (bool disposing)
			{
			}

		private Encoding GuessEncoding (string s)
			{
			return ContentType.GuessEncoding (s);
			}

		#endregion // Methods
		}
	}

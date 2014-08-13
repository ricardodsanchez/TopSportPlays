using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mandrill;

namespace TheTopPlays.Services
{
    /// <summary>
    /// Allows application to send emails using Mandrill's API.
    /// https://mandrillapp.com/api/docs/
    /// </summary>
    public class EmailServices
    {
        private const string Apikey = "YOUR_API_KEY";
        private const string UserName = "YOUR_USER_NAME";
        string _username = UserName;
        string _password = Apikey;
        public string Username { get { return this._username; } set { this._username = value; } }
        public string Password { get { return this._password; } set { this._password = value; } }

        public IEnumerable<EmailAddress> To { get; set; }
        public EmailAddress From { get; set; }
        public string FromDisplayName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public void SendEmail()
        {
            //create a new message object
            var message = new MandrillApi(Apikey, true);

            //send the mail
            try
            {
                var emailMessage = new EmailMessage()
                {
                    to = To,
                    subject = Subject,
                    raw_message = Message,
                    from_email = From.email,
                    from_name = From.name
                };

                //message.SendRawMessage(emailMessage);

                message.SendMessage(To, Subject, Message, From, new DateTime());
            }
            catch (System.Exception ex)
            {
                throw;
                // log the error
            }
        }
    }
}

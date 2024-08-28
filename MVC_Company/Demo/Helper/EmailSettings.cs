using Demo.DAL.Models;
using System.Net.Mail;
using System.Net;

namespace Demo.PL.Helper
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			//mail server : gmail
			var client = new SmtpClient(host: "smtp.gmail.com", port: 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential(userName:"mohamedashraf10901@gmail.com",password: "putlaoogsavchhet");
			
			client.Send("mohamedashraf10901@gmail.com", email.Recipients, email.Subject, email.Body);


		}
	}
}

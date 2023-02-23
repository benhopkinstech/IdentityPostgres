using IdentityPostgres.Data.Tables;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace IdentityPostgres.Classes
{
    public class MailHelper
    {
        public static short DetermineMailProviderId(string providerName)
        {
            return providerName.ToUpper() switch
            {
                "SENDGRID" => (short)Enums.MailProvider.SendGrid,
                _ => -1,
            };
        }

        public static string DetermineMailProviderName(short providerId)
        {
            return providerId switch
            {
                (short)Enums.MailProvider.SendGrid => "SendGrid",
                _ => "",
            };
        }

        public static short DetermineMailTypeId(string typeName)
        {
            return typeName.ToUpper() switch
            {
                "TEST" => (short)Enums.MailType.Test,
                "EMAIL VERIFICATION" => (short)Enums.MailType.EmailVerification,
                _ => -1,
            };
        }

        public static string DetermineMailTypeName(short typeId)
        {
            return typeId switch
            {
                (short)Enums.MailType.Test => "Test",
                (short)Enums.MailType.EmailVerification => "Email Verification",
                _ => "",
            };
        }

        public static async Task<bool> SendMailAsync(ConfigMail mail, Enums.MailType mailType, string recipient, string url)
        {
            return mail.ProviderId switch
            {
                (short)Enums.MailProvider.SendGrid => await UseSendGridAsync(mail, mailType, recipient),
                _ => false,
            };
        }

        private static async Task<bool> UseSendGridAsync(ConfigMail mail, Enums.MailType mailType, string recipient)
        {
            var client = new SendGridClient(mail.ApiKey);
            var from = new EmailAddress(mail.Email, mail.Name);
            var to = new EmailAddress(recipient);
            SendGridMessage message = SendGrid.Helpers.Mail.MailHelper.CreateSingleEmail(from, to, GenerateSubject(mailType), GeneratePlainText(mailType), GenerateHtml(mailType));
            var response = await client.SendEmailAsync(message);
            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        private static string GenerateSubject(Enums.MailType mailType)
        {
            return mailType switch
            {
                Enums.MailType.Test => "Email Configured",
                Enums.MailType.EmailVerification => "Verify your email",
                _ => "",
            };
        }

        private static string GeneratePlainText(Enums.MailType mailType)
        {
            return mailType switch
            {
                Enums.MailType.Test => "Test mail sent from Identity Postgres",
                Enums.MailType.EmailVerification => "Thanks for registering, please confirm your email address",
                _ => "",
            };
        }

        private static string GenerateHtml(Enums.MailType mailType)
        {
            return mailType switch
            {
                Enums.MailType.Test => "Test mail sent from <strong>Identity Postgres</strong>",
                Enums.MailType.EmailVerification => "Thanks for registering, please confirm your <i>email address</i>",
                _ => "",
            };
        }
    }
}

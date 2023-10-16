using Desafio_Balta_IBGE.Domain.Interfaces.Services;
using Desafio_Balta_IBGE.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Balta_IBGE.Infra.Services
{
    public sealed class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task SendVerificationEmail(User user)
        {
            var host = _configuration["SMTP:Host"];
            var nome = _configuration[key: "SMTP:Nome"];
            var username = _configuration[key: "SMTP:UserName"];
            var senha = _configuration[key: "SMTP:Senha"];
            var porta = Convert.ToInt32(_configuration[key: "SMTP:Porta"]);

            var mail = new MailMessage()
            {
                From = new MailAddress(address: username, displayName: nome)
            };

            mail.To.Add(user.Email.Address);

            mail.IsBodyHtml = true;
            
            mail.Priority = MailPriority.High;
            
            mail.Subject = $"Ativação da conta {user.Name}";
            
            mail.Body = $"<strong><p>Olá {user.Name}!</p></strong>" +
                        $"<p>Seu código para ativação da sua conta é: {user.Email.VerifyEmail.Code}</p>" +
                        $"<p>Caso já tenha ativado sua conta, por favor desconsidere este e-mail.</p>" +
                        $"<p><br /><br />Equipe Guilherme e George</p>";


            using var smtp = new SmtpClient(host: host, port: porta);
            
            smtp.Credentials = new NetworkCredential(userName: username, password: senha);
            
            smtp.EnableSsl = true;

            smtp.Send(message: mail);
        }

        public async Task SendActivationSuccess(User user)
        {
            var host = _configuration["SMTP:Host"];
            var nome = _configuration[key: "SMTP:Nome"];
            var username = _configuration[key: "SMTP:UserName"];
            var senha = _configuration[key: "SMTP:Senha"];
            var porta = Convert.ToInt32(_configuration[key: "SMTP:Porta"]);

            var mail = new MailMessage()
            {
                From = new MailAddress(address: username, displayName: nome)
            };

            mail.IsBodyHtml = true;

            mail.Priority = MailPriority.High;

            mail.To.Add(user.Email.Address);

            mail.Subject = $"Conta ativada!";

            mail.Body = $"<strong><p>Olá, {user.Name}!</strong></p>" +
                        $"<p>Seja bem vindo!</p>" +
                        $"<p>Sua conta foi ativada com sucesso!<p>" +
                        $"<br><br>" +
                        $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";

            

            using var smtp = new SmtpClient(host: host, port: porta);
            smtp.Credentials = new NetworkCredential(userName: username, password: senha);

            smtp.EnableSsl = true;

            smtp.Send(message: mail);
        }
    }
}

using System.Net.Mail;

namespace Desafio_Balta_IBGE.Shared.Exceptions
{
    public class SmtpServerException : Exception
    {
        public SmtpServerException(SmtpStatusCode statusCode) : base(GetErrorMessage(statusCode))
        {
            //var error = GetErrorMessage(statusCode);
            StatusCode = statusCode;
        }

        public SmtpStatusCode StatusCode { get; }

        private static readonly Dictionary<SmtpStatusCode, string> ErrorMessages = new Dictionary<SmtpStatusCode, string>
        {
            [SmtpStatusCode.GeneralFailure] = "Erro geral ao enviar o email. Por favor, contate o suporte.",
            [SmtpStatusCode.MailboxBusy] = "O servidor de email está ocupado. Por favor, tente novamente mais tarde.",
            [SmtpStatusCode.TransactionFailed] = "Falha ao processar a transação. Por favor, tente novamente mais tarde ou entre em contato com o suporte.",
            [SmtpStatusCode.SystemStatus] = "Erro no status do sistema do servidor de email. Por favor, contate o suporte.",
            [SmtpStatusCode.HelpMessage] = "Erro ao recuperar a mensagem de ajuda do servidor de email. Tente novamente mais tarde.",
            [SmtpStatusCode.ServiceNotAvailable] = "O serviço de email não está disponível no momento. Por favor, tente novamente mais tarde.",
            [SmtpStatusCode.MailboxNameNotAllowed] = "O nome da caixa de correio do destinatário não é permitido. Verifique o endereço de email e tente novamente.",
            [SmtpStatusCode.CommandParameterNotImplemented] = "O parâmetro do comando SMTP não é suportado pelo servidor de email. Por favor, revise as configurações e tente novamente.",
            [SmtpStatusCode.MustIssueStartTlsFirst] = "É necessário iniciar uma conexão segura antes de enviar o email. Por favor, ative a segurança TLS/SSL na configuração do seu cliente de email.",
            [SmtpStatusCode.SyntaxError] = "Erro de sintaxe no comando SMTP. Verifique os detalhes do seu email e tente novamente."
        };



        public static string GetErrorMessage(SmtpStatusCode statusCode)
        {
            foreach (var (code, message) in ErrorMessages)
            {
                if (code == statusCode)
                {
                    return message;
                }
            }

            return $"Falha ao tentar enviar o e-mail com código de status: {statusCode}.";
        }
    }
}

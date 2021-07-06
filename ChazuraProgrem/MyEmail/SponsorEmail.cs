using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChazuraProgram.Models;
using ChazuraProgram.EOReadFiles;
using System.Text;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ChazuraProgram.MyEmail
{
    public class SponsorEmail
    {
        private SponsorData Sponsor { get; }
        private LinkGenerator LinkGenerator { get; }
       
        private HostString Host { get; }

        const string Path = @".\wwwroot\EmailHTML\";
        const string CnfrmSpnsr = @"EmailSponsorConfirm.txt";
        const string CnfrmSpnsrSubject = "Confirming sponsorship";
        private string HtmlText;
        private string errorMsg;
        
        public SponsorEmail(SponsorData sponsor,LinkGenerator linkGenerator,  HostString host)
        {
            Sponsor = sponsor;
            LinkGenerator = linkGenerator;
            Host = host;
        }
        public EmailInfo GetEmailConfirming()
        {
            if (Sponsor.Payment == null || Sponsor.User == null)
            {
                return new EmailInfo { GotIt = false, ErrorMsg = "Missing information to process the email." };
            }
            bool gotIt = CreateHtml4ConfirmSponsor();
            if (gotIt)
            {
                return new EmailInfo
                {
                    EmailBody = HtmlText,
                    GotIt = true,
                    Subject = CnfrmSpnsrSubject,
                    Address = Sponsor.User.Email
                };
            }
            else
            {
                return new EmailInfo
                {
                    GotIt = false,
                    ErrorMsg = errorMsg
                };
            }
        }
        private string CnfrmSpnsrfullPath => Path + CnfrmSpnsr;
        private string Linker(LinkGenerator linkGenerator,int sponsorId)
        {
            string host = Host.ToString();
            string protocol = @"https://";
            string link = linkGenerator.GetPathByAction("CheckStatus", "Sponsor", new { sponsorId });
            return protocol+ host+link;
        }
        private bool CreateHtml4ConfirmSponsor()
        {
            StringBuilder text;
            try
            {
                text =new StringBuilder( GetFile.Read(CnfrmSpnsrfullPath));
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return false;
            }
            try
            {
                text.Replace("zz-name", Sponsor.User.FirstName + " " + Sponsor.User.LastName);
                text.Replace("zz-date", Sponsor.Date.ToShortDateString());
                text.Replace("zz-paydate", Sponsor.Payment?.DateCharged?.ToShortDateString() ?? Sponsor.Date.ToShortDateString());
                text.Replace("zz-amt", Sponsor.Payment?.Amount.ToString("c"));
                text.Replace("zz-cardtype", Sponsor.Payment?.CardType.ToString());
                string link = Linker(LinkGenerator, Sponsor.SponsId);
                text.Replace("zz-link", link);
                string cardNum = Sponsor.Payment?.CC_Number ?? "????";
                cardNum = cardNum[^4..];
                text.Replace("zz-ccnumber", cardNum);
                HtmlText = text.ToString();
                return true;
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return false;
            }

        }

        
    }
}

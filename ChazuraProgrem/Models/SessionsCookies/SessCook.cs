using Microsoft.AspNetCore.Http;

namespace ChazuraProgram.Models
{
    public class SessCook : ISessCook
    {
        private ISession Session { get; set; }
        //private IRequestCookieCollection CookieReq { get; set; }
        //private IResponseCookies CookieRsp { get; set; }
        public SessCook(IHttpContextAccessor context)
        {
            Session = context.HttpContext.Session;
            //CookieReq = context.HttpContext.Request.Cookies;
            //CookieRsp = context.HttpContext.Response.Cookies;
        }
        //const string UserNameKey = "usernamekey";
        const string UserObgKey = "userobgkey";
        const string backFromDetailKey = "detBack";
        const string RouteDetailKey = "routedetail";
        const string UserAndPaymentInfoKey = "userpaykey";
        const string AdminLogIntoUser = "loginuser";
        const string RouteKey = "currentroute";

        public void SetUserObgInSession(User user) => Session.SetObject<User>(UserObgKey, user);
        public User GetUserObgFromSession() => Session.GetObject<User>(UserObgKey);
        //public void SetUserNameInCookie(string name) => CookieRsp.SetString(UserNameKey, name);
        //public string GetNameFromCookie() => CookieReq.GetString(UserNameKey);
        public void ClearUsersCookSess()
        {
            //CookieRsp.Delete(UserNameKey);
            Session.Remove(UserObgKey);
        }
        public void SetRedirectFromDetails(int count) => Session.SetInt32(backFromDetailKey, count);
        public int GetRedirectFromDetails() => Session.GetInt32(backFromDetailKey).GetValueOrDefault();
        public void ClearRedirectFromDetails() => Session.Remove(backFromDetailKey);

        public void ClearRoutesDetail() => Session.Remove(RouteDetailKey);

        public void SetRoutesDetail(RouteDictionary route) => Session.SetObject<RouteDictionary>(RouteDetailKey, route);

        public RouteDictionary GetRoutesDetail() => Session.GetObject<RouteDictionary>(RouteDetailKey);
        public void SetSponsor(SponsorInfo info) => Session.SetObject<SponsorInfo>(UserAndPaymentInfoKey, info);
        public SponsorInfo GetSponsor() => Session.GetObject<SponsorInfo>(UserAndPaymentInfoKey);
        public void ClearSponsor() => Session.Remove(UserAndPaymentInfoKey);
        public void ClearSession() => Session.Clear();
        public void SetAdminLoIntoUserAcct(User user) => Session.SetObject<User>(AdminLogIntoUser, user);
        public User GetAdminLoIntoUserAcc() => Session.GetObject<User>(AdminLogIntoUser);
        public void ClearAdminLoIntoUserAcc() => Session.Remove(AdminLogIntoUser);
        public void SetRouteGrid(RouteDictionary routes) => Session.SetObject<RouteDictionary>(RouteKey, routes);
        public RouteDictionary GetRouteGrid() => Session.GetObject<RouteDictionary>(RouteKey);
        public void ClearRouteGrid() => Session.Remove(RouteKey);

    }
}

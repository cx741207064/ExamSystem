using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Bll.Organiz
{
    public class UserCenter
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="_examid"></param>
        /// <param name="_cardid"></param>
        /// <param name="_vcode"></param>
        /// <param name="_vcodeCookie"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static object login(string _uid, string _pwd, string _vcode, string _vcodeCookie, ref string error)
        {
            if (string.IsNullOrEmpty(_vcodeCookie))
            {
                error = "验证码已失效";
            }
            else if (_vcodeCookie != Untity.HelperSecurity.MD5(_vcode.ToLower()))
            {
                error = "验证码错误";
            }
            else
            {
                Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
                if (_orga == null)
                {
                    error = "账号或者密码错误，请重新输入";
                }
            }
            return 1;
        }

        public static object wxlogin(string _classid, ref string uid, ref string pwd, ref string error)
        {

            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_classid);
            if (_orga == null)
            {
                error = "机构尚未录入，请联系客服";
            }
            else
            {
                uid = _orga.Name;
                pwd = _orga.Password;
            }
            return 1;
        }

        /// <summary>
        /// 刷新用户信息
        /// </summary>
        /// <param name="_examid"></param>
        /// <param name="_cardid"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static object userinfo(string _uid, string _pwd, ref string error)
        {

            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            return _orga;
        }
    }
}

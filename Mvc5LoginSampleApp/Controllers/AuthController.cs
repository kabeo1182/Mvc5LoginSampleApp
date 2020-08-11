using mdlDatabase;
using Mvc5LoginSampleApp.Models;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace Mvc5LoginSampleApp.Controllers
{
    public class AuthController : Controller
    {
        /// <summary>
        /// ログイン 表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(AuthModel model)
        {
            if (ChkLogin(model.Id,model.Password))
            {
                // ユーザー認証 失敗
                FormsAuthentication.SetAuthCookie(model.Id, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // ユーザー認証 失敗
                this.ModelState.AddModelError(string.Empty, "指定されたユーザー名またはパスワードが正しくありません。");
                return this.View(model);
            }
        }

        /// <summary>
        /// ログアウト処理
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Auth", "Index");
        }
    
        private bool ChkLogin(string id ,string pass) 
        {
            DatabaseOpn db = new DatabaseOpn();
            SqlDataReader sqlRdr = null;
            StringBuilder strSql = new StringBuilder();

            string userid = String.Empty;
            string userpass = String.Empty;

            userid = id;
            userpass = pass;

            strSql.Append(" SELECT ");
            strSql.Append(" USERID ");
            strSql.Append(" ,USERPASS ");

            strSql.Append(" FROM ");
            strSql.Append(" USERINFO ");

            strSql.Append(" WHERE ");
            strSql.Append(" USERID = '" + userid + "'");
            strSql.Append(" AND USERPASS = '" + userpass + "'");

            try
            {
                if (!db.DB_Connect())//DB接続
                {
                    return false;
                }

                if (!db.DB_SqlReader(strSql.ToString(), ref sqlRdr))//データ取得
                {
                    return false;
                }

                if (!sqlRdr.HasRows)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (sqlRdr != null)
                {
                    if (!sqlRdr.IsClosed)
                    {
                        sqlRdr.Close();
                    }
                }
                db.DB_Close();//DB切断
            }
        }

    }
}
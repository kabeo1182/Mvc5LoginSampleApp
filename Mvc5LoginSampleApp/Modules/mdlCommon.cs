using System;
using System.IO;
using System.Text;
using System.Data.SqlClient;

using mdlDatabase;

namespace mdlCommon
{
    public class CmnOpn
    {

        DatabaseOpn db = new DatabaseOpn();
        mdlConst.CONSTOpn cnst = new mdlConst.CONSTOpn();



        public string GET_headLine()
        {
            SqlDataReader sqlRdr = null;
            StringBuilder strSql = new StringBuilder();

            string strHeadLine = "";

            strSql.Append("SELECT b.big_urlroot, s.small_urlroot, k.kiji_id, k.title, k.description, k.ogimage, k.ins_date ");
            strSql.Append("FROM m_kiji k ");
            strSql.Append("INNER JOIN c_big_category b ");
            strSql.Append("ON k.big_category_id = b.big_category_id ");
            strSql.Append("INNER JOIN c_small_category s ");
            strSql.Append("ON k.big_category_id = s.big_category_id ");
            strSql.Append("AND k.small_category_id = s.small_category_id ");
            strSql.Append("ORDER BY k.ins_date DESC ");

            try
            {
                if (!db.DB_Connect())//DB接続
                {
                    return "";
                }

                if (!db.DB_SqlReader(strSql.ToString(), ref sqlRdr))//データ取得
                {
                    return "";
                }

                //while (sqlRdr.Read())
                //{
                //    strHeadLine += "<a href="\""" +="" sqlrdr["big_urlroot"].tostring()="" " = "" sqlrdr["small_urlroot"].tostring() = "" sqlrdr["kiji_id"].tostring() = "" "\"="" class="\"linkstyle\"" >\n";
                //    strHeadLine += " <div class="\"headlinecontainer\"" >\n";
                //    strHeadLine += "  <div class="\"headlineimage\"" >< img src = "\"/Content/image/"" +="" sqlrdr["ogimage"].tostring()="" "\"=""></div>\n";
                //    strHeadLine += "  <div class="\"headlinetitle\"" > " + sqlRdr["title"].ToString() + " </ div >\n";
                //    strHeadLine += "  <div class="\"headlinedescription\"" > " + sqlRdr["description"].ToString() + " </ div >\n";
                //    strHeadLine += "  <div class="\"headlinedate\"" > " + sqlRdr["ins_date"].ToString() + " </ div >\n";
                //    strHeadLine += " </div>\n";
                //    strHeadLine += "</a>\n\n";
                //}

                return strHeadLine;
            }
            catch (Exception ex)
            {
                return "";
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
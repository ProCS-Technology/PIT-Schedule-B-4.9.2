using Microsoft.AspNet.SignalR;
using ProcsDLL.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ProcsDLL
{
    public class ChatHub : Hub
    {
        static List<ChatRooms> rooms = new List<ChatRooms>();
        static List<Messages> CurrentMessage = new List<Messages>();

        public void Connect(Int32 CompanyId, Int32 CommitteeId, Int32 MeetingId, Int32 AgendaId, string ChatId, String uName)
        {
            var id = Context.ConnectionId;

            if (rooms.Count(x => x.CompanyId == CompanyId && x.CommitteeId == CommitteeId && x.MeetingId == MeetingId && x.AgendaId == AgendaId && x.ChatId == ChatId) == 0)
            {
                string logintime = DateTime.Now.ToString();

                DataTable dtUsr = GetUserInfo(uName, CompanyId);
                DataTable dtMsg = GetChatMsgs(ChatId, CompanyId);
                string sUrl = ConfigurationManager.AppSettings["Url"];

                ChatRooms room = new ChatRooms { CompanyId = CompanyId, CommitteeId = CommitteeId, MeetingId = MeetingId, AgendaId = AgendaId, ChatId = ChatId, LoginTime = logintime };
                CUser user = new CUser { ConnectionId = id, LoginId = uName, UserName = Convert.ToString(dtUsr.Rows[0]["USER_NM"]), UserImage = sUrl + Convert.ToString(dtUsr.Rows[0]["UPLOAD_AVATAR"]), LoginTime = logintime };
                room.AddUser(user);
                rooms.Add(room);

                string sMsgs = "";
                if (dtMsg.Rows.Count > 0)
                {
                    foreach (DataRow drMsg in dtMsg.Rows)
                    {
                        if (Convert.ToString(drMsg["USER_LOGIN"]) == uName)
                        {
                            sMsgs += "<li class='out'>";
                        }
                        else
                        {
                            sMsgs += "<li class='in'>";
                        }
                        sMsgs += "<img class='avatar' alt='' src='" + sUrl + Convert.ToString(drMsg["UPLOAD_AVATAR"]) + "' />";
                        sMsgs += "<div class='message'>";
                        sMsgs += "<span class='arrow'> </span>";
                        sMsgs += "<a href='javascript:;' class='name'>" + Convert.ToString(drMsg["USER_NM"]) + "</a>";
                        sMsgs += "<span class='datetime'> at " + Convert.ToString(drMsg["DT"]) + "</span>";
                        sMsgs += "<span class='body'>" + Convert.ToString(drMsg["CHAT_MESSAGE"]) + "</span>";
                        sMsgs += "</div>";
                        sMsgs += "</li>";
                    }
                }
                Clients.Client(id).GetChatMessage(id, sMsgs);
            }
            else
            {
                var o = rooms.FirstOrDefault(x => x.CompanyId == CompanyId && x.CommitteeId == CommitteeId && x.MeetingId == MeetingId && x.AgendaId == AgendaId && x.ChatId == ChatId);
                if (o != null)
                {
                    if (o.users.Count(x => x.UserName == uName) == 0)
                    {
                        string logintime = DateTime.Now.ToString();

                        DataTable dtUsr = GetUserInfo(uName, CompanyId);
                        DataTable dtMsg = GetChatMsgs(ChatId, CompanyId);
                        string sUrl = ConfigurationManager.AppSettings["Url"];
                        CUser user = new CUser { ConnectionId = id, LoginId = uName, UserName = Convert.ToString(dtUsr.Rows[0]["USER_NM"]), UserImage = sUrl + Convert.ToString(dtUsr.Rows[0]["UPLOAD_AVATAR"]), LoginTime = logintime };
                        o.AddUser(user);

                        string sMsgs = "";
                        if (dtMsg.Rows.Count > 0)
                        {
                            foreach (DataRow drMsg in dtMsg.Rows)
                            {
                                if (Convert.ToString(drMsg["USER_LOGIN"]) == uName)
                                {
                                    sMsgs += "<li class='out'>";
                                }
                                else
                                {
                                    sMsgs += "<li class='in'>";
                                }
                                sMsgs += "<img class='avatar' alt='' src='" + sUrl + Convert.ToString(drMsg["UPLOAD_AVATAR"]) + "' />";
                                sMsgs += "<div class='message'>";
                                sMsgs += "<span class='arrow'> </span>";
                                sMsgs += "<a href='javascript:;' class='name'>" + Convert.ToString(drMsg["USER_NM"]) + "</a>";
                                sMsgs += "<span class='datetime'> at " + Convert.ToString(drMsg["DT"]) + "</span>";
                                sMsgs += "<span class='body'>" + Convert.ToString(drMsg["CHAT_MESSAGE"]) + "</span>";
                                sMsgs += "</div>";
                                sMsgs += "</li>";
                            }
                        }
                        Clients.Client(id).GetChatMessage(id, sMsgs);
                    }
                }
            }
        }
        public void Send(string name, string message)
        {
            Clients.All.sendMessage(name, message);
        }
        public void Broadcast(string loginId, string message, string ChatId, Int32 UserId, Int32 CompanyId)
        {
            string sConStr = SQLHelper.GetConnString();
            string sUrl = ConfigurationManager.AppSettings["Url"];
            DataTable dtUsr = GetUserInfo(loginId, CompanyId);
            using (SqlConnection sCon = new SqlConnection(sConStr))
            {
                SqlCommand sCmd = new SqlCommand();
                sCon.Open();
                //sCon.ChangeDatabase("PROCS_BOARD_MEETING");
                sCmd.Connection = sCon;
                string _sql = "SELECT C.DATABASE_NAME FROM PROCS_BUSINESS_COMPANY(NOLOCK) A " +
                    "INNER JOIN PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) B ON A.COMPANY_ID=B.COMPANY_ID " +
                    "INNER JOIN PROCS_MODULE(NOLOCK) C ON B.MODULE_ID=C.MODULE_ID AND C.MODULE_NM='Board Meeting' WHERE A.COMPANY_ID=" + CompanyId;
                sCmd.CommandText = _sql;
                string sModuleDb = Convert.ToString(sCmd.ExecuteScalar());

                _sql = "SELECT CHATID FROM " + sModuleDb + "..BMS_AGENDA_CHAT(NOLOCK) WHERE DEVICE_UNIQUE_ID='" + ChatId + "'";
                sCmd.CommandText = _sql;
                Int32 iChatId = Convert.ToInt32(sCmd.ExecuteScalar());

                _sql = "INSERT INTO " + sModuleDb + "..BMS_AGENDA_CHAT_MESSAGES(CHATID,USER_ID,CHAT_MESSAGE,REPLIED_ON,DEVICE_UNIQUE_ID) VALUES(" +
                    iChatId + "," + UserId + ",'" + message + "',GETDATE(),'" + ChatId + "')";
                sCmd.CommandType = CommandType.Text;
                sCmd.CommandText = _sql;
                sCmd.ExecuteNonQuery();
            }
            Clients.All.Broadcast(Convert.ToString(dtUsr.Rows[0]["USER_NM"]), message, ChatId, Convert.ToString(dtUsr.Rows[0]["UPLOAD_AVATAR"]), loginId);
        }
        public DataTable GetUserInfo(string loginId, Int32 CompanyId)
        {
            string sConStr = SQLHelper.GetConnString();
            string sUrl = ConfigurationManager.AppSettings["Url"];
            using (SqlConnection sCon = new SqlConnection(sConStr))
            {
                SqlCommand sCmd = new SqlCommand();
                sCon.Open();
                //sCon.ChangeDatabase("PROCS_BOARD_MEETING");
                sCmd.Connection = sCon;
                string _sql = "SELECT C.DATABASE_NAME FROM PROCS_BUSINESS_COMPANY(NOLOCK) A " +
                    "INNER JOIN PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) B ON A.COMPANY_ID=B.COMPANY_ID " +
                    "INNER JOIN PROCS_MODULE(NOLOCK) C ON B.MODULE_ID=C.MODULE_ID AND C.MODULE_NM='Board Meeting' WHERE A.COMPANY_ID=" + CompanyId;
                sCmd.CommandText = _sql;
                string sModuleDb = Convert.ToString(sCmd.ExecuteScalar());

                _sql = "SELECT B.USER_NM,('" + sUrl + "'+A.UPLOAD_AVATAR) AS UPLOAD_AVATAR FROM " + sModuleDb + "..PROCS_BMS_USER(NOLOCK) A " +
                    "INNER JOIN PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID WHERE A.COMPANY_ID=" + CompanyId + " AND A.USER_LOGIN = '" + loginId + "'";
                sCmd.CommandText = _sql;
                DataSet dsUsr = new DataSet();
                SqlDataAdapter daUsr = new SqlDataAdapter(sCmd);
                daUsr.Fill(dsUsr);
                DataTable dtUsr = new DataTable();
                dtUsr = dsUsr.Tables[0];
                return dtUsr;
            }
        }
        public DataTable GetChatMsgs(string sChatId, Int32 iCompanyId)
        {
            string sConStr = SQLHelper.GetConnString();
            string sUrl = ConfigurationManager.AppSettings["Url"];
            using (SqlConnection sCon = new SqlConnection(sConStr))
            {
                SqlCommand sCmd = new SqlCommand();
                sCon.Open();
                sCmd.Connection = sCon;
                string _sql = "SELECT C.DATABASE_NAME FROM PROCS_BUSINESS_COMPANY(NOLOCK) A " +
                    "INNER JOIN PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) B ON A.COMPANY_ID=B.COMPANY_ID " +
                    "INNER JOIN PROCS_MODULE(NOLOCK) C ON B.MODULE_ID=C.MODULE_ID AND C.MODULE_NM='Board Meeting' WHERE A.COMPANY_ID=" + iCompanyId;
                sCmd.CommandText = _sql;
                string sModuleDb = Convert.ToString(sCmd.ExecuteScalar());

                //sCon.ChangeDatabase(sModuleDb);

                _sql = "SELECT A.USER_ID,A.CHAT_MESSAGE,A.REPLIED_ON,B.UPLOAD_AVATAR,C.USER_NM,B.USER_LOGIN,CONVERT(VARCHAR,A.REPLIED_ON,0) AS DT " +
                    "FROM " + sModuleDb + "..BMS_AGENDA_CHAT_MESSAGES(NOLOCK) A " +
                    "INNER JOIN " + sModuleDb + "..PROCS_BMS_USER(NOLOCK) B ON A.USER_ID=B.ID " +
                    "INNER JOIN PROCS_USERS(NOLOCK) C ON B.USER_LOGIN=C.LOGIN_ID WHERE A.DEVICE_UNIQUE_ID='" + sChatId + "' ORDER BY REPLIED_ON";
                sCmd.CommandText = _sql;
                DataSet dsChats = new DataSet();
                SqlDataAdapter daChats = new SqlDataAdapter(sCmd);
                daChats.Fill(dsChats);
                DataTable dtChats = new DataTable();
                dtChats = dsChats.Tables[0];
                return dtChats;
            }
        }
    }
}
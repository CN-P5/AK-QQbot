using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Threading;
using System.Windows.Forms;
using QQLite.Framework;
using QQLite.Framework.Dapper;
using QQLite.Framework.QQEnum;
using QQLite.Framework.Event;
using QQLite.Framework.SDK;
using QQLite.Framework.Tool;
using System.Collections.Generic;

namespace QQLite.Plugin
{
    public class InterfacePlugin : QQLite.Framework.SDK.Plugin
    {
        public InterfacePlugin()
        {
            // 插件名
            this.PluginName = "Web接口插件";
            // 插件简介
            this.Description = "将收到的信息POST到接口地址";
            // 插件说明
            this.Note = "将收到的信息POST到接口地址，停用插件后，机器人将不会使用接口，和轮循";
            // 插件版本
            this.Version = new Version("4.0.0.0");
            // 插件发布地址
            this.PluginUrl = License.SoftWareUrl;
            // 插件开发者
            this.Author = License.SoftWareAuthor;
            // 开发者地址
            this.AuthorUrl = License.SoftWareUrl;
            // 开发者QQ
            this.AuthorQQ = License.SoftWareQQ;
        }

        #region IPlugin
        private PluginSetting _setting;
        public PluginSetting Setting
        {
            get { return this._setting ?? (this._setting = PluginSetting.GetSetting(this)); }
            set { _setting = value; }
        }

        /// <summary>
        /// 安装插件
        /// </summary>
        /// <returns>null：安装插件成功，string：错误信息</returns>
        public override string Install()
        {
            if (MessageBox.Show("这个是Web接口插件，需要配合Web接口使用\n如果你没有Web接口请不要安装该插件。\n是否继续安装？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return "错误";
            }
            else
            {
                this.Setting = PluginSetting.GetSetting(this);
                this.Setting.AddConfig("InterfaceUrl", "接口文件", "");
                this.Setting.AddConfig("ApiKey", "接口密匙", "");
                this.Setting.AddConfig("InterfaceCharSet", "接口文件编码", "UTF-8");
                this.Setting.AddConfig("ApiPort", "监听端口", 0);
                this.Setting.AddConfig("ApiGetMin", "接口获取间隔最小值", 50);
                this.Setting.AddConfig("ApiGetMax", "接口获取间隔最大值", 60);
                this.Setting.Import();
                return null;
            }
        }

        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <returns>null：卸载插件成功，string：错误信息</returns>
        public override string UnInstall()
        {
            return null;
        }

        /// <summary>
        /// 升级插件
        /// </summary>
        /// <param name="version">数据库记录的版本</param>
        /// <returns>null：升级插件成功，string：错误信息</returns>
        public override string Update(Version version)
        {
            this.Install();
            return null;
        }

        /// <summary>
        /// 运行插件
        /// </summary>
        /// <returns>null：运行插件成功，string：错误信息</returns>
        public override string Start()
        {
            this.Set();

            this.SDK = new QQClientSDK();
            this.SDK.SetAllowSendMessage += SDK_SetAllowSendMessage;

            //this.SDK.LoginStatusChange += SDK_LoginStatusChange;
            this.SDK.LoginWrongPassword += SDK_LoginWrongPassword;
            this.SDK.LoginNeedActivation += SDK_LoginNeedActivation;
            this.SDK.LoginSuccess += SDK_LoginSuccess;
            this.SDK.LoginFail += SDK_LoginFail;

            this.SDK.ReceiveNormalIM += SDK_ReceiveNormalIM;
            this.SDK.ReceiveVibration += SDK_ReceiveVibration;
            this.SDK.ReceiveInputState += SDK_ReceiveInputState;
            this.SDK.ReceiveTempSessionIM += SDK_ReceiveTempSessionIM;
            this.SDK.FriendStatusChange += SDK_FriendStatusChange;
            this.SDK.FriendSignatureChange += SDK_FriendSignatureChange;
            this.SDK.FriendReceiveAdd += SDK_FriendReceiveAdd;

            this.SDK.ReceiveClusterIM += SDK_ReceiveClusterIM;
            this.SDK.ReceiveDiscussionIM += SDK_ReceiveDiscussionIM;
            this.SDK.ClusterInviteMe += SDK_ClusterInviteMe;
            this.SDK.ClusterMeJoin += SDK_ClusterMeJoin;
            this.SDK.ClusterRequestJoin += SDK_ClusterRequestJoin;
            this.SDK.ClusterMemberGag += SDK_ClusterMemberGag;
            this.SDK.ClusterMemberJoin += SDK_ClusterMemberJoin;
            this.SDK.ClusterRoleChange += SDK_ClusterRoleChange;
            this.SDK.ClusterMemberExit += SDK_ClusterMemberExit;
            this.SDK.ClusterMemberKick += SDK_ClusterMemberKick;
            this.SDK.ClusterCardChange += SDK_ClusterCardChange;
            this.SDK.ClusterXml += SDK_ClusterXml;
            this.SDK.VerifyCode += SDK_VerifyCode;

            return null;
        }

        /// <summary>
        /// 停止运行插件
        /// </summary>
        /// <returns>null：停止插件成功，string：错误信息</returns>
        public override string Stop()
        {
            return null;
        }

        /// <summary>
        /// 显示插件窗体
        /// </summary>
        /// <returns>null：先生窗口成功，string：错误信息</returns>
        public override string ShowForm()
        {
            new SettingForm(this).ShowDialog();
            return null;
        }

        /// <summary>
        /// 异常的处理
        /// </summary>
        /// <param name="e">异常</param>
        public override void ProcessException(Exception e)
        {
        }
        #endregion

        #region 设置
        private string InterfaceUrl { get; set; }
        private string InterKey { get; set; }
        private int InterPort { get; set; }
        private Encoding InterfaceCharSet { get; set; }

        internal void Set()
        {
            this.InterfaceUrl = this.Setting.GetConfig("InterfaceUrl");
            this.InterKey = this.Setting.GetConfig("ApiKey");
            this.InterPort = this.Setting.GetConfig("ApiPort");
            this.InterfaceCharSet = Encoding.GetEncoding(this.Setting.GetConfig("InterfaceCharSet") ?? "UTF-8");
        }
        #endregion



        void SDK_VerifyCode(object sender, VerifyCodeEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
        }

        void SDK_ClusterXml(object sender, ClusterXmlEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.ClusterInfo.ExternalId.ToString());
            sParaTemp.Add("GroupName", e.ClusterInfo.Name);
            sParaTemp.Add("Sender", e.MemberQQ.ToString());
            sParaTemp.Add("SenderName", e.MemberName);
            sParaTemp.Add("XML", e.Xml);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendClusterIM(e.ClusterInfo.ClusterId, callback);
                e.Cancel = true;
            }
        }

        void SDK_ClusterCardChange(object sender, ClusterCardChangeEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.ClusterInfo.ExternalId.ToString());
            sParaTemp.Add("GroupName", e.ClusterInfo.Name);
            sParaTemp.Add("Sender", e.MemberQQ.ToString());
            sParaTemp.Add("SenderName", e.OldCard);
            sParaTemp.Add("NewCard", e.NewCard);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendClusterIM(e.ClusterInfo.ClusterId, callback);
                e.Cancel = true;
            }
        }

        void SDK_ClusterMemberKick(object sender, ClusterMemberKickEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.ClusterInfo.ExternalId.ToString());
            sParaTemp.Add("GroupName", e.ClusterInfo.Name);
            sParaTemp.Add("Sender", e.MemberQQ.ToString());
            sParaTemp.Add("SenderName", e.MemberName);
            sParaTemp.Add("Operator", e.Operator.ToString());
            sParaTemp.Add("OperatorName", e.OperatorName);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendClusterIM(e.ClusterInfo.ClusterId, callback);
                e.Cancel = true;
            }
        }

        void SDK_ClusterMemberExit(object sender, ClusterMemberExitEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.ClusterInfo.ExternalId.ToString());
            sParaTemp.Add("GroupName", e.ClusterInfo.Name);
            sParaTemp.Add("Sender", e.MemberQQ.ToString());
            sParaTemp.Add("SenderName", e.MemberName);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendClusterIM(e.ClusterInfo.ClusterId, callback);
                e.Cancel = true;
            }
        }

        void SDK_ClusterRoleChange(object sender, ClusterRoleChangeEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.ClusterInfo.ExternalId.ToString());
            sParaTemp.Add("GroupName", e.ClusterInfo.Name);
            sParaTemp.Add("Sender", e.MemberQQ.ToString());
            sParaTemp.Add("SenderName", e.MemberName);
            sParaTemp.Add("IsSetAdmin", e.IsSetAdmin ? "1" : "0");

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendClusterIM(e.ClusterInfo.ClusterId, callback);
                e.Cancel = true;
            }
        }

        void SDK_ClusterMemberJoin(object sender, ClusterMemberJoinEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.ClusterInfo.ExternalId.ToString());
            sParaTemp.Add("GroupName", e.ClusterInfo.Name);
            sParaTemp.Add("Sender", e.MemberQQ.ToString());
            sParaTemp.Add("SenderName", e.MemberName);
            sParaTemp.Add("Operator", e.Operator.ToString());
            sParaTemp.Add("OperatorName", e.OperatorName);
            sParaTemp.Add("Message", e.Message);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendClusterIM(e.ClusterInfo.ClusterId, callback);
                e.Cancel = true;
            }
        }

        void SDK_ClusterMemberGag(object sender, ClusterMemberGagEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.ClusterInfo.ExternalId.ToString());
            sParaTemp.Add("GroupName", e.ClusterInfo.Name);
            sParaTemp.Add("Sender", e.MemberQQ.ToString());
            sParaTemp.Add("SenderName", e.MemberName);
            sParaTemp.Add("Operator", e.Operator.ToString());
            sParaTemp.Add("OperatorName", e.OperatorName);
            sParaTemp.Add("SendTime", e.SendTime.GetTimeMillis().ToString());
            sParaTemp.Add("GagTime", e.GagTime.ToString());

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendClusterIM(e.ClusterInfo.ClusterId, callback);
                e.Cancel = true;
            }
        }

        void SDK_ClusterRequestJoin(object sender, ClusterRequestJoinEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.ClusterInfo.ExternalId.ToString());
            sParaTemp.Add("GroupName", e.ClusterInfo.Name);
            sParaTemp.Add("Sender", e.MemberQQ.ToString());
            sParaTemp.Add("SenderName", e.MemberName);
            sParaTemp.Add("Message", e.Message);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                if (callback == "1")
                {
                    this.Client.ClusterApproveJoin(e.ClusterInfo.ClusterId, e.MemberQQ);
                    e.Cancel = true;
                }
                else if (callback == "2")
                {
                    this.Client.ClusterRejectJoin(e.ClusterInfo.ClusterId, e.MemberQQ, "");
                    e.Cancel = true;
                }
            }
        }

        void SDK_ClusterMeJoin(object sender, ClusterMeJoinEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.ClusterInfo.ExternalId.ToString());
            sParaTemp.Add("GroupName", e.ClusterInfo.Name);
            sParaTemp.Add("Sender", e.Operator.ToString());
            sParaTemp.Add("SenderName", e.OperatorName);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendClusterIM(e.ClusterInfo.ClusterId, callback);
                e.Cancel = true;
            }
        }

        void SDK_ClusterInviteMe(object sender, ClusterInviteMeEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.ExternalId.ToString());
            sParaTemp.Add("Sender", e.Operator.ToString());
            sParaTemp.Add("SenderName", e.OperatorName);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                if (callback == "0") //同意
                {
                    this.Client.ClusterApproveInviteJoin(e.ClusterId, e.Operator);
                    e.Cancel = true;
                }
                else if (callback == "1") //拒绝
                {
                    this.Client.ClusterRejectInviteJoin(e.ClusterId, e.Operator, "");
                    e.Cancel = true;
                }
            }
        }

        void SDK_ReceiveDiscussionIM(object sender, DiscussionIMEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.GroupId.ToString());
            sParaTemp.Add("Message", e.Message);
            sParaTemp.Add("Sender", e.Sender.ToString());
            sParaTemp.Add("SenderName", e.SenderName);
            sParaTemp.Add("SendTime", e.SendTime.GetTimeMillis().ToString());

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendDiscussionIM(e.GroupId, callback);
                e.Cancel = true;
            }
        }

        void SDK_ReceiveClusterIM(object sender, ClusterIMEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("GroupId", e.ClusterInfo.ExternalId.ToString());
            sParaTemp.Add("GroupName", e.ClusterInfo.Name);
            sParaTemp.Add("Sender", e.Sender.ToString());
            sParaTemp.Add("SenderName", e.SenderName);
            sParaTemp.Add("SendTime", e.SendTime.GetTimeMillis().ToString());
            sParaTemp.Add("Message", e.Message);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendClusterIM(e.ClusterInfo.ClusterId, callback);
                e.Cancel = true;
            }
        }

        void SDK_FriendReceiveAdd(object sender, FriendInfoEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("Sender", e.FriendInfo.QQ.ToString());
            sParaTemp.Add("SenderName", e.FriendInfo.ShowName);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendIM(e.FriendInfo.QQ, callback);
                e.Cancel = true;
            }
        }

        void SDK_FriendSignatureChange(object sender, FriendInfoEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("Sender", e.FriendInfo.QQ.ToString());
            sParaTemp.Add("SenderName", e.FriendInfo.ShowName);
            sParaTemp.Add("Signature", e.FriendInfo.Signature);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendIM(e.FriendInfo.QQ, callback);
                e.Cancel = true;
            }
        }

        void SDK_FriendStatusChange(object sender, FriendInfoEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("Sender", e.FriendInfo.QQ.ToString());
            sParaTemp.Add("SenderName", e.FriendInfo.ShowName);
            sParaTemp.Add("Status", Enum.GetName(typeof(QQStatus), e.FriendInfo.Status));

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendIM(e.FriendInfo.QQ, callback);
                e.Cancel = true;
            }
        }

        void SDK_ReceiveTempSessionIM(object sender, TempSessionIMEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("Sender", e.Sender.ToString());
            sParaTemp.Add("SenderName", e.SenderName);
            sParaTemp.Add("SendTime", e.SendTime.GetTimeMillis().ToString());
            sParaTemp.Add("Message", e.Message);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendTempIM(e.Sender, callback, e.Token);
                e.Cancel = true;
            }
        }

        void SDK_ReceiveInputState(object sender, InputStateEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("Sender", e.Sender.ToString());
            sParaTemp.Add("SenderName", e.SenderName);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendIM(e.Sender, callback);
                e.Cancel = true;
            }
        }

        void SDK_ReceiveVibration(object sender, VibrationEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("Sender", e.Sender.ToString());
            sParaTemp.Add("SenderName", e.SenderName);
            sParaTemp.Add("SendTime", e.SendTime.GetTimeMillis().ToString());
            sParaTemp.Add("First", e.First ? "1" : "0");

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendIM(e.Sender, callback);
                e.Cancel = true;
            }
        }

        void SDK_ReceiveNormalIM(object sender, NormalIMEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));
            sParaTemp.Add("Sender", e.Sender.ToString());
            sParaTemp.Add("SenderName", e.SenderName);
            sParaTemp.Add("SendTime", e.SendTime.GetTimeMillis().ToString());
            sParaTemp.Add("Message", e.Message);

            var callback = this.BuildRequest(sParaTemp);
            if (!string.IsNullOrEmpty(callback))
            {
                this.Client.SendIM(e.Sender, callback);
                e.Cancel = true;
            }
        }


        void SDK_LoginFail(object sender, LoginStatusChangeEventArgs e)
        {
            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));

            this.BuildRequest(sParaTemp);
        }

        void SDK_LoginNeedActivation(object sender, LoginStatusChangeEventArgs e)
        {
            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));

            this.BuildRequest(sParaTemp);
        }

        void SDK_LoginWrongPassword(object sender, LoginStatusChangeEventArgs e)
        {
            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));

            this.BuildRequest(sParaTemp);
        }

        //void SDK_LoginStatusChange(object sender, LoginStatusChangeEventArgs e)
        //{
        //    Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
        //    sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));

        //    this.BuildRequest(sParaTemp);
        //}

        void SDK_LoginSuccess(object sender, LoginStatusChangeEventArgs e)
        {
            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", Enum.GetName(typeof(PluginEvent), e.Event));

            this.BuildRequest(sParaTemp);
        }

        void SDK_SetAllowSendMessage(object sender, SetAllowSendMessageEventArgs e)
        {
            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("Event", "Setting");
            sParaTemp.Add("Type", Enum.GetName(typeof(SendMessageType), e.Type));
            sParaTemp.Add("IsAllow", e.IsAllow ? "1" : "0");

            this.BuildRequest(sParaTemp);
        }

        private string BuildRequest(Dictionary<string, string> sParaTemp)
        {
            if (string.IsNullOrEmpty(this.InterfaceUrl))
            {
                return null;
            }
            if (!sParaTemp.ContainsKey("RobotQQ"))
            {
                sParaTemp.Add("RobotQQ", this.Client.QQ.ToString());
            }
            if (!string.IsNullOrEmpty(this.InterKey) && !sParaTemp.ContainsKey("Key"))
            {
                sParaTemp.Add("Key", this.InterKey);
            }
            if (this.InterPort > 0)
            {
                sParaTemp.Add("Port", this.InterPort.ToString());
            }
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in sParaTemp)
            {
                prestr.Append(temp.Key + "=" + HttpUtility.UrlEncode(temp.Value, this.InterfaceCharSet) + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            //待请求参数数组字符串
            var http = new HttpHelper();
            http.Url = this.InterfaceUrl;
            http.PostData = prestr.ToString();
            http.Method = Method.POST;
            http.UserAgent = "Qlwz_QQLite_" + License.Version;
            http.AcceptEncoding = "gzip, deflate";
            http.AcceptLanguage = "zh-cn";
            http.Accept = "*/*";
            http.Do();
            if (http.StatusCode == HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(http.Html))
                {
                    if (string.Compare(http.Html, "PostNull", true) != 0
                        && string.Compare(http.Html, "ErrNull", true) != 0)
                    {
                        return http.Html;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        private string Post_Http(string url, string postData, Encoding charSet)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            byte[] byteArray;
            if (!string.IsNullOrEmpty(this.InterKey))
            {
                postData = string.Format("Key={0}&Port={1}&Version={2}&{3}", this.InterKey, this.InterPort, License.Version, postData);
            }
            byteArray = charSet.GetBytes(postData.Trim());
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Headers["Accept-Language"] = "zh-cn";
                httpWebRequest.UserAgent = "Qlwz_QQLite_" + License.Version;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Method = "POST";
                httpWebRequest.KeepAlive = true;
                httpWebRequest.Accept = "*/*";
                httpWebRequest.ContentLength = byteArray.Length;
                Stream dataStream = httpWebRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string callBack = null;
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream(), charSet))
                    {
                        callBack = streamReader.ReadToEnd();
                    }
                    if (string.IsNullOrEmpty(callBack))
                    {
                        return null;
                    }
                    else
                    {
                        return callBack;
                    }
                }
                else
                {
                    this.OnLog(string.Format("PostErr : StatusCode：{0}", httpWebResponse.StatusCode.ToString()));
                }
                return callBack;
            }
            catch (Exception ex)
            {
                this.OnLog(string.Format("PostErr : Message：{0}", ex.Message));
                return null;
            }
            finally
            {
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
            }
        }
    }
}

using System;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace QQLite.Plugin
{
    public partial class SettingForm : Form
    {
        public InterfacePlugin Plugin { get; set; }
        public SettingForm(InterfacePlugin plugin)
        {
            InitializeComponent();
            this.Icon = QQLite.Framework.License.SoftIcon;
            this.Plugin = plugin;
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            this.txt_Set_ApiUrl.Text = this.Plugin.Setting.GetConfig("InterfaceUrl");
            this.rbtn_Set_ApiGB2312.Checked = string.Compare(this.Plugin.Setting.GetConfig("InterfaceCharSet"), "GB2312", true) == 0;
            this.rbtn_Set_ApiUTF8.Checked = !this.rbtn_Set_ApiGB2312.Checked;
            this.txt_Set_ApiKey.Text = this.Plugin.Setting.GetConfig("ApiKey");
            this.txt_Set_ApiPort.Text = this.Plugin.Setting.GetConfig("ApiPort").ToString();
            this.txt_Set_ApiGetMin.Text = this.Plugin.Setting.GetConfig("ApiGetMin").ToString();
            this.txt_Set_ApiGetMax.Text = this.Plugin.Setting.GetConfig("ApiGetMax").ToString();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string apiUrl = this.txt_Set_ApiUrl.Text.Trim();
            string charSet = this.rbtn_Set_ApiGB2312.Checked ? "GB2312" : "UTF-8";
            string apiKey = this.txt_Set_ApiKey.Text.Trim();
            int apiPort = (int)this.txt_Set_ApiPort.Value;
            int apiGetMin;
            int apiGetMax;
            if (!int.TryParse(this.txt_Set_ApiGetMin.Text, out apiGetMin))
            {
                MessageBox.Show("请求接口频率只能为数字", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(this.txt_Set_ApiGetMax.Text, out apiGetMax))
            {
                MessageBox.Show("请求接口频率只能为数字", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (apiGetMin > apiGetMax)
            {
                MessageBox.Show("请求接口频率最小值不能大于最大值", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Plugin.Setting.UpdateConfig("InterfaceUrl", apiUrl);
            this.Plugin.Setting.UpdateConfig("InterfaceCharSet", charSet);
            this.Plugin.Setting.UpdateConfig("ApiKey", apiKey);
            this.Plugin.Setting.UpdateConfig("ApiPort", apiPort);
            this.Plugin.Setting.UpdateConfig("ApiGetMin", apiGetMin);
            this.Plugin.Setting.UpdateConfig("ApiGetMax", apiGetMax);
            if (this.Plugin.Setting.Update())
            {
                MessageBox.Show("保存设置成功", "保存设置成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存设置失败", "保存设置失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            this.Plugin.Set();
        }
    }
}

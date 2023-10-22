using System.Net;

namespace DWinFormsThreadException
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Mangle the URL t get the exception is WorkerQotD.
        string urlQotD = "http://nvp-functions.azurewebsites.net/api/qotd?slow=true";

        void WorkerQotD()
        {
            try
            {
                WebClient client = new WebClient();
                var s = client.DownloadString(urlQotD);
                Invoke((Action)(() => tbQotD.Text = s));
            }
            catch (Exception x)
            {
                Invoke((Action)(() => tbQotD.Text = x.Message));
            }
        }

        private void bGetQotd_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(WorkerQotD);
            t.Start();
        }
    }
}
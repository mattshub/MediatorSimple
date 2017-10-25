using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediatR;

namespace MediatorTest
{
    public partial class Form1 : Form
    {
        private readonly IMediator mMediator;

        public Form1()
        {
            mMediator = IoCConfiguration.BuildMediator();

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AsyncUpdateGreeting();
            txtGreeting.Text = "...";
        }

        private async void AsyncUpdateGreeting()
        {
            Task<GreetingResponse> task = mMediator.Send(new GreetingRequest(txtName.Text));
            await task;
            txtGreeting.Text = task.Result.Message;
        }
    }
}

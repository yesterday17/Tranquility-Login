﻿using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tranquility_Login
{
    public partial class DownloadForm : Form
    {
        private static Form frmDownload;
        private static Label label_download;
        private static Label label_size;
        private static ProgressBar progress_download;

        private Boolean clone_finished
        {
            get
            {
                return false;
            }
            set
            {
                System.Environment.Exit(0);
            }
        }

        delegate void textTip(double receive, double total);

        public DownloadForm()
        {
            init();            
        }

        public DownloadForm(String text)
        {
            init();
            this.Text = text;
        }

        private void init()
        {
            InitializeComponent();
            frmDownload = this;
            label_download = this.lb_download;
            label_size = this.lb_size;
            progress_download = this.prog_download;
        }

        public static bool TransferProgress(TransferProgress progress)
        {
            //MessageBox.Show($"Objects: {progress.ReceivedObjects} of {progress.TotalObjects}, Bytes: {progress.ReceivedBytes}");

            textTip textEdit = delegate (double receive, double total)
            {
                label_download.Text = (receive / total * 100).ToString("F2") + "%";
                label_size.Text = $"已下载：{progress.ReceivedBytes / 1024 / 1024} MB";
                progress_download.Value = (int)(receive / total * 100);
            };
            label_download.Invoke(textEdit, progress.ReceivedObjects, progress.TotalObjects);

            //label_download.Text = (progress.ReceivedObjects/progress.TotalObjects).ToString("#0.00") + "%";
            return true;
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {
            Task clone = new Task(CloneMethod);
            clone.Start();
        }

        private void CloneMethod()
        {
            try
            {
                CloneOptions co = new CloneOptions
                {
                    BranchName = "master",
                    OnTransferProgress = DownloadForm.TransferProgress,
                    CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials { Username = "yesterday17", Password = "001206" }
                };

                Repository.Clone(Constants.git_repository, Constants.path, co);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("请尝试重新启动该程序！");
                System.Environment.Exit(0);
            }
            MessageBox.Show("下载完成！");
            clone_finished = true;
        }
    }
}

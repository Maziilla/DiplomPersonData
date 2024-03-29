﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace ClientPersonData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeInterface();
        }
        private BindingList<Interes> dgvAddInteres = new BindingList<Interes>();
        private BindingList<Interes> dgvEditInteres = new BindingList<Interes>();

        private void InitializeInterface()
        {
            btTargFind.Enabled = false;
            btAddApply.Enabled = false;
            btAddInteres.Enabled = false;
            dgvAddInteres = new BindingList<Interes>();
            dgvEditInteres = new BindingList<Interes>();
            dgvAddInterests.DataSource = dgvAddInteres;
            dgvEditInterests.DataSource = dgvEditInteres;
            dgvAddInterests.Columns[0].HeaderText = "Название";
            dgvAddInterests.Columns[1].HeaderText = "Оценка";
            dgvEditInterests.Columns[0].HeaderText = "Название";
            dgvEditInterests.Columns[1].HeaderText = "Оценка";
            dgvTargFindProf.Columns.Add("cName", "Приложение");
            dgvTargFindProf.Columns.Add("cCount", "Количество пользователей");
            //dgvTargFindProf.Columns[0].HeaderText = "Компания";
            //dgvTargFindProf.Columns[1].HeaderText = "Приложение пользователей";
        }

        private void ДобавитьДанныеToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void ДобавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvAddInteres = new BindingList<Interes>();
            panelAddData.Visible = true;
            panelEditData.Visible = false;
            panelDelete.Visible = false;
            panelTarg.Visible = false;
            //Add
            btAddApply.Enabled = false;
            btAddInteres.Enabled = false;
            dgvAddInterests.DataSource = dgvAddInteres;
            dgvAddInterests.ReadOnly = true;
            Guid.NewGuid();
        }
        private void ИзменитьИнтересыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelAddData.Visible = false;
            panelEditData.Visible = true;
            panelDelete.Visible = false;
            panelTarg.Visible = false;
            btEditAddInteres.Enabled = false;
            btEditApply.Enabled = false;
            dgvEditInteres = new BindingList<Interes>();
            dgvEditInterests.DataSource = dgvEditInteres;
            dgvEditInterests.ReadOnly = true;
            dgvEditInterests.AllowUserToAddRows = false;

        }

        private void УдалитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelAddData.Visible = false;
            panelEditData.Visible = false;
            panelTarg.Visible = false;
            panelDelete.Visible = true;
            btDeleteApply.Enabled = false;

        }
        private void ТаргетингToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelAddData.Visible = false;
            panelEditData.Visible = false;
            panelDelete.Visible = false;
            panelTarg.Visible = true;            
            btTargCheckTransfer.Enabled = false;
            tbTargIdTrans.Text = "3f3405c6f2fd42cf1adcb82fbe0b562a90e6756ab42d9aa225263cbe75f2ce98";
            tbTargNumBlock.Text = "32142119";
            tbTargNameAdvetirsment.Text = "moneyaccount";
            tbTargQuantity.Text = "1,2000";
        }

        private void TbTargNameInter_TextChanged(object sender, EventArgs e)
        {
            if (tbTargNameInter.Text != "")
                btTargFind.Enabled = true;
            else
                btTargFind.Enabled = false;
        }

        private void TbTargRateInter_TextChanged(object sender, EventArgs e)
        {
            if (tbTargNameInter.Text != "")
                btTargFind.Enabled = true;
            else
                btTargFind.Enabled = false;
        }

        private void BtTargFind_Click(object sender, EventArgs e)
        {

            //dgvTargFindProf.Rows.Add("Oduvanchiki", "7");
            //dgvTargFindProf.Rows.Add("FindFriend", "1");
            //dgvTargFindProf.Rows.Add("FacePhone", "13");
            string site = "http://maz-VirtualBox:3000/api/selectPersonByInteres";
            WebRequest request = WebRequest.Create(site);
            request.Method = "POST";
            string postData = "{ \"$class\": \"org.example.mynetwork.selectPersonByInteres\", \"name\":\"" + tbTargNameInter.Text + "\", \"rate\": \"" + nudTargRateInter.Value.ToString() + "\" }";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            //request.Headers.Add("Accept: application/json");

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();

            using (dataStream = response.GetResponseStream())
            {
                cbTargSelsectAppl.Items.Clear();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadLine();
                List<Result> persons = JsonConvert.DeserializeObject<List<Result>>(responseFromServer);
                //rtbTargLists.Text += "Количество подходящих записей = "+ persons.Count.ToString();
                foreach (var person in persons)
                {
                    dgvTargFindProf.Rows.Add(person.nameOrg, person.count);
                    cbTargSelsectAppl.Items.Add(person.nameOrg);
                    //  rtbTargLists.Text += person.UserID;
                }
                // rtbTargLists.Text += responseFromServer;
                //rtbTargLists.Text += persons[0].UserID;                
            }
            //request.Headers.Add("Content-Type: application/json");


        }

        private void TbAddNameInteres_TextChanged(object sender, EventArgs e)
        {
            if (tbAddNameInteres.Text != "")
                btAddInteres.Enabled = true;
            else
                btAddInteres.Enabled = false;
        }

        private void BtAddInteres_Click(object sender, EventArgs e)
        {
            Interes newInter = new Interes();
            newInter.nameInteres = tbAddNameInteres.Text;
            newInter.rate = Decimal.ToInt32(nudAddRateInteres.Value);
            dgvAddInteres.Add(newInter);
            CheckAddForm();
            //dgvAddInterests.Rows.Add(newInter.nameInteres, newInter.rate);
        }
        public void CheckAddForm()
        {
            if (tbAddCountry.Text != "" && tbAddPublAdr.Text != "" && dgvAddInteres.Count != 0 &&tbAddAppl.Text!="")
                btAddApply.Enabled = true;
            else
                btAddApply.Enabled = false;
        }
        public void CheckEditForm()
        {
            if (tbEditGUID.Text != "" && dgvEditInteres.Count != 0)
                btEditApply.Enabled = true;
            else
                btEditApply.Enabled = false;
        }
        private void TbAddPublAdr_TextChanged(object sender, EventArgs e)
        {
            CheckAddForm();
        }

        private void TbAddCountry_TextChanged(object sender, EventArgs e)
        {
            CheckAddForm();
        }

        private void BtAddApply_Click(object sender, EventArgs e)
        {
            string site = "http://maz-VirtualBox:3000/api/addData";
            WebRequest request = WebRequest.Create(site);
            request.Method = "POST";

            string strWithInterests = "[";
            int countData = 0;
            foreach (var item in dgvAddInteres)
            {
                if (countData != 0)
                    strWithInterests += ",";
                strWithInterests += "{\"$class\":\"org.example.mynetwork.Interes\",\"nameinteres\": \"" + item.nameInteres + "\", \"rate\": " + item.rate.ToString() + "}";
                if (countData == dgvAddInteres.Count - 1)
                    strWithInterests += "]";
                countData++;
            }
            string postData = "{\"$class\": \"org.example.mynetwork.addData\", \"nameApplication\":\""+ tbAddAppl.Text +"\", \"id\": \"" + Guid.NewGuid().ToString()+"\", \"count\": \"" + tbAddCountry.Text + "\", \"publadr\": \"" + tbAddPublAdr.Text + "\", \"interess\": "+ strWithInterests+"}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            //request.Headers.Add("Accept: application/json");

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();

            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                MessageBox.Show("Succsess");
            }
        }

        private void TbDeleteGUID_TextChanged(object sender, EventArgs e)
        {
            if(tbDeleteGUID.Text!="")
            {
                btDeleteApply.Enabled = true;
            }
            else
            {
                btDeleteApply.Enabled = false;
            }
        }

        private void BtDeleteApply_Click(object sender, EventArgs e)
        {
            string site = "http://maz-VirtualBox:3000/api/deleteData";
            WebRequest request = WebRequest.Create(site);
            request.Method = "POST";
            string postData = "{ \"$class\": \"org.example.mynetwork.deleteData\", \"id\":\"" + tbDeleteGUID.Text + "\" }";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            //request.Headers.Add("Accept: application/json");

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();

            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                MessageBox.Show("Удаление прошло успешно");
            }
        }

        private void TbEditGUID_TextChanged(object sender, EventArgs e)
        {
            CheckEditForm();
        }

        private void TbEditInterName_TextChanged(object sender, EventArgs e)
        {
            if (tbEditInterName.Text != "")
                btEditAddInteres.Enabled = true;
            else
                btEditAddInteres.Enabled = false;
        }

        private void BtEditAddInteres_Click(object sender, EventArgs e)
        {
            Interes newInter = new Interes();
            newInter.nameInteres = tbEditInterName.Text;
            newInter.rate = Decimal.ToInt32(nudEditRate.Value);
            dgvEditInteres.Add(newInter);
            CheckEditForm();
        }

        private void BtEditApply_Click(object sender, EventArgs e)
        {
            string site = "http://maz-VirtualBox:3000/api/changeInteres";
            WebRequest request = WebRequest.Create(site);
            request.Method = "POST";

            string strWithInterests = "[";
            int countData = 0;
            foreach (var item in dgvEditInteres)
            {
                if (countData != 0)
                    strWithInterests += ",";
                strWithInterests += "{\"$class\":\"org.example.mynetwork.Interes\",\"nameinteres\": \"" + item.nameInteres + "\", \"rate\": " + item.rate.ToString() + "}";
                if (countData == dgvEditInteres.Count - 1)
                    strWithInterests += "]";
                countData++;
            }
            string postData = "{\"$class\": \"org.example.mynetwork.changeInteres\", \"interess\": " + strWithInterests + ", \"id\": \"" + tbEditGUID.Text + "\"}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            //request.Headers.Add("Accept: application/json");

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();

            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                MessageBox.Show("Изменение интересов успешно");
            }
        }
      
        private void CheckPay(string from, string to, double price, string idTrasaction, string numBlock)
        {
            //request.Headers.Add("Accept: application/json");
            Stream dataStream=null;

            WebResponse response=null;
            bool flag = true;
            while (flag)
            {
                string site = "http://jungle2.cryptolions.io:80/v1/history/get_transaction";
                WebRequest request = WebRequest.Create(site);
                request.Method = "POST";
                string postData = "{\"id\":\"3f3405c6f2fd42cf1adcb82fbe0b562a90e6756ab42d9aa225263cbe75f2ce98\",\"block_num_hint\":32142119}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                try
                {
                    response = request.GetResponse();
                    flag = false;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }

            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                MessageBox.Show(responseFromServer);

                //responseFromServer.Replace("\\", "");

                //var action = responseFromServer.Substring()
                int start = responseFromServer.IndexOf("{\"from");
                int stop = responseFromServer.IndexOf(",\"memo\"");
                string intrestingString = responseFromServer.Substring(start,stop-start)+"}";
                //var answer = JObject.Parse(responseFromServer);
                intrestingString = intrestingString.Replace(" EOS", "");                
                data persons = JsonConvert.DeserializeObject<data>(intrestingString);                
                MessageBox.Show("from " + persons.from + " to " + persons.to + " count " + persons.quantity);
                if (persons.from == from && persons.to == to && persons.quantity == price)
                    MessageBox.Show("Оплата подтверждена");
                else
                    MessageBox.Show("Факт оплаты не подтверждён");

           }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            CheckPay(tbTargNameAdvetirsment.Text,cbTargSelsectAppl.SelectedItem.ToString(), Convert.ToDouble(tbTargQuantity.Text),tbTargIdTrans.Text,tbTargNumBlock.Text);
        }

        private void TbTargQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(tbTargQuantity.Text);
                btTargCheckTransfer.Enabled = true;
            }
            catch(Exception ex)
            {
                btTargCheckTransfer.Enabled = false;
            }
        }

        private void ВыборкаПоИнтересамToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TbAddAppl_TextChanged(object sender, EventArgs e)
        {
            CheckAddForm();
        }
    }
}

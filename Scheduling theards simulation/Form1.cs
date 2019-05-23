using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduling_theards_simulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[,] array = new int[10, 4];
        int count = 0;
        int numOfProcesses;
        Boolean done = false;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public Boolean CheckDone()
        {
            int tot = 0;
            for (int i = 0; i < 9; i++)
            {
                tot += array[i, 1];
            }
            if (tot < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public String GetProcess(int pro)
        {
            switch (pro)
            {
                case 0:
                    return "A";
                case 1:
                    return "B";
                case 2:
                    return "C";
                case 3:
                    return "D";
                case 4:
                    return "E";
                case 5:
                    return "F";
                case 6:
                    return "G";
                case 7:
                    return "H";
                case 8:
                    return "I";
                case 9:
                    return "J";
            }
            return "a";

        }
        private void button2_Click(object sender, EventArgs e)
{
            int arrive = Convert.ToInt32(txtArr.Text);
            int burst = Convert.ToInt32(txtBur.Text);
            array[count, 0] = arrive;
            array[count, 1] = burst;
            array[count, 3] = 1;
            if(comboBox2.SelectedIndex <= 0)
            {
                array[count, 2] = 0;
            }
            else
            {
                array[count, 2] = comboBox2.SelectedIndex + 1;
            }
            
            if (count < (numOfProcesses - 1))
            {
                listBox1.Items.Add(GetProcess(count) + "\t" + arrive + "\t" + burst);
                count++;
                lblProcess.Text = "Enter Arrival/ Burst Time for process:" + GetProcess(count);
                txtArr.Text = "";
                txtBur.Text = "";
                txtArr.Select();
            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = true;
                listBox1.Items.Add(GetProcess(count) + "\t" + arrive + "\t" + burst);
            }
            comboBox2.SelectedIndex = 0;

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public int RandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            numOfProcesses = (comboBox1.SelectedIndex + 1);
            listBox1.Items.Add("Process \t Arrival \t Burst");
            lblProcess.Text = "Enter Arrival/ Burst Time for process:" + GetProcess(count);
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            int totCicle = 0;
            for(int o = 0; o < numOfProcesses; o++ )
            {
                totCicle += array[o, 1]; 
            }
            int progress = 98/totCicle;
            listBox2.Visible = true;
            listBox2.Items.AddRange(listBox1.Items);
            listBox1.Items.Clear();
            int cLoop = 0;
            int c2Loop = 0;
            
            while (!done)
            {
                
                for (int i = 0; i < numOfProcesses; i++)
                {
                    if (array[i, 0] <= cLoop)
                    {
                        if (array[i, 1] != 0)
                        {
                            if(array[i,2] > 2)
                            {
                                for(int k = 0; k <= array[i,1]; k++)
                                {
                                    array[i, 1] -= 1;
                                    listBox1.Items.Add(GetProcess(i) + (array[i, 1]));
                                    await Task.Delay(500);
                                    cLoop++;
                                    c2Loop++;
                                    progressBar1.Increment(progress);
                                    label5.Text = progressBar1.Value.ToString() + "%";
                                }
                            }
                            else
                            {
                                array[i, 1] -= 1;
                                listBox1.Items.Add(GetProcess(i) + (array[i, 1]));
                                await Task.Delay(500);
                                cLoop++;
                                c2Loop++;
                                progressBar1.Increment(progress);
                                label5.Text = progressBar1.Value.ToString() + "%";
                            }
                        }
                        else
                        {
                            //listBox1.Items.Add("----" + cLoop);
                            c2Loop--;
                            progressBar1.Increment(progress);
                            cLoop++;
                            int doneCount = 0;
                            for (int j = 0; j < numOfProcesses; j++)
                            {
                                if (array[j, 1] == 0)
                                {
                                    doneCount += 1;
                                }
                            }
                            if (doneCount == numOfProcesses)
                            {
                                listBox1.Items.Add("Done");
                                progressBar1.Increment(progress);
                                done = true;
                            }
                        }
                    }
                    else
                    {
                        cLoop++;
                        int doneCount = 0;
                        for(int j = 0; j < numOfProcesses; j++)
                        {
                            if(array[j,1] == 0)
                            {
                                doneCount += 1;
                            }
                        }
                        if(doneCount == numOfProcesses)
                        {
                            listBox1.Items.Add("Done");
                            progressBar1.Increment(progress);
                            done = true;
                        }

                    }
                    if(i >= (numOfProcesses -1))
                    {
                        i = -1;
                    }
                    if(done)
                    {
                        i = 11;
                    }
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Visible = true;
            button2.Enabled = true;
            label2.Visible = true;
            button3.Visible = true;
            button4.Visible = false;
            groupBox1.Visible = true;
            button1.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Visible = false;
            label2.Visible = false;
            button4.Visible = true;
            button3.Visible = false;
            groupBox1.Visible = true;
            button1.Enabled = false;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            int totCicle = 0;
            for (int o = 0; o < numOfProcesses; o++)
            {
                totCicle += array[o, 1];
            }
            int progress = 98 / totCicle;
            listBox2.Visible = true;
            listBox2.Items.AddRange(listBox1.Items);
            listBox1.Items.Clear();
            int cLoop = 0;
            int TQ = 0;
            
            while (!done)
            {

                for (int i = 0; i < numOfProcesses; i++)
                {
                    if (array[i, 0] <= cLoop)
                    {
                        if (array[i, 1] != 0)
                        {
                            if (array[i, 2] > 2)
                            {
                                for (int k = 0; k <= array[i, 1]; k++)
                                {
                                    array[i, 1] -= 1;
                                    listBox1.Items.Add(GetProcess(i) + (array[i, 1]));
                                    await Task.Delay(500);
                                    progressBar1.Increment(progress);
                                    label5.Text = progressBar1.Value.ToString() + "%";
                                    cLoop++;
                                }
                            }
                            else
                            {
                                if(array[i,3] == 1)
                                {
                                    array[i, 1] -= 1;
                                    array[i, 3] += 1;
                                    listBox1.Items.Add(GetProcess(i) + (array[i, 1]));
                                    await Task.Delay(500);
                                    progressBar1.Increment(progress);
                                    label5.Text = progressBar1.Value.ToString() + "%";
                                    cLoop++;
                                }
                                else
                                {for(int k = 0; k < 1; k++)
                                    {
                                        if (array[i, 3] == 2)
                                        {
                                            array[i, 1] -= 1;
                                            array[i, 3] += 1;
                                            listBox1.Items.Add(GetProcess(i) + (array[i, 1]));
                                            await Task.Delay(500);
                                            progressBar1.Increment(progress);
                                            label5.Text = progressBar1.Value.ToString() + "%";
                                            cLoop++;
                                        }
                                    }
                                    for (int k = 0; k < 2; k++)
                                    {
                                        if (array[i, 3] == 3)
                                        {
                                            array[i, 1] -= 1;
                                            listBox1.Items.Add(GetProcess(i) + (array[i, 1]));
                                            await Task.Delay(500);
                                            progressBar1.Increment(progress);
                                            label5.Text = progressBar1.Value.ToString() + "%";
                                            cLoop++;
                                        }
                                    }
                                        
                                }
                               
                            }
                        }
                        else
                        {
                            //listBox1.Items.Add("----" + cLoop);
                            cLoop++;
                            progressBar1.Increment(progress);
                            int doneCount = 0;
                            for (int j = 0; j < numOfProcesses; j++)
                            { 
                                if (array[j, 1] <= 0)
                                {
                                    doneCount += 1;
                                }
                            }
                            if (doneCount == numOfProcesses)
                            {
                                listBox1.Items.Add("Done");
                                progressBar1.Increment(progress);
                                label5.Text = progressBar1.Value.ToString() + "%";
                                done = true;
                            }
                        }
                    }
                    else
                    {
                        int doneCount = 0;
                        for (int j = 0; j < numOfProcesses; j++)
                        {
                            if (array[j, 1] <= 0)
                            {
                                doneCount += 1;
                            }
                        }
                        if (doneCount == numOfProcesses)
                        {
                            listBox1.Items.Add("Done");
                            progressBar1.Increment(progress);
                            label5.Text = progressBar1.Value.ToString() + "%";
                            done = true;
                        }
                    }
                    if (i >= (numOfProcesses - 1))
                    {
                        i = -1;
                    }
                    if (done)
                    {
                        i = 11;
                    }
                }
            }
        }
    }
}

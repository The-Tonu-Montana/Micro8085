using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micro8085
{
    public partial class Micro8085 : Form
    {
        public Micro8085()
        {
            InitializeComponent();
        }

        object[] mem = new object[1000];


        public int PROG_CNT = 0;

        public int limit;

        public string temp, str;
        public string str2 = "";
        public int storecount = 0;
        int j = 0;

        int tstate = 0;
        int bytes = 0;
        int Mcycle = 0;
       


        public void Updatemem()
        {
            MEMORY.Text = null;
            for (int i = 0; i < 999; i++)
            {
                string s = dtohex(i);

                int len = 4 - s.Length;
                for (int j = 0; j < len; j++)
                    s = "0" + s;

                if (mem[i].ToString() != "xx")
                    MEMORY.Text = MEMORY.Text + s + "    :    " + mem[i].ToString() + "\r\n";
            }
        }

        private void STORE_Click(object sender, EventArgs e)
        {
            if (j == 0)
            {
                for (j = 0; j < 999; j++)
                {
                    mem[j] = "xx";
                }
            }

            storecount++;
            if (temp != null && temp.Length > 1)
            {
                mem[PROG_CNT] = temp.Substring(0, 2);
            }
            PROG_CNT++;
            //ADDR.Text = "";
            temp = null;
            //str = textBox1.Text + PROG_CNT + "   :   " + mem[PROG_CNT++] + "\r\n";

            //textBox1.Text = str;

            if (mem[PROG_CNT].ToString() != "xx")
            {
                storecount--;
            }

            //          MEMORY.Text = null;                     // BS
            //          str2 = null;
            //          limit = storecount;
            //          for (int i = 0; i < limit && mem[i].ToString() != "xx"; i++)
            //           {
            //               str2 = str2 + i + " :     " + mem[i] + "\r\n";
            //           }
            //          MEMORY.Text = str2;

            Updatemem();
            ADDR.Text = null;

            string Tex = dtohex(PROG_CNT);
            int lim = Tex.Length;
            lim = 4 - lim;

            for (int i = 0; i < lim; i++)
                Tex = "0" + Tex;


            DT.Text = Tex;

        }


        public void send(string x)
        {
            temp += x;
            ADDR.Text = temp;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            send("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            send("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            send("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            send("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            send("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            send("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            send("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            send("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            send("9");
        }

        private void button0_Click(object sender, EventArgs e)
        {
            send("0");
        }

        private void buttonA_Click(object sender, EventArgs e)
        {
            send("A");
        }

        private void buttonB_Click(object sender, EventArgs e)
        {
            send("B");
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            send("C");
        }

        private void buttonD_Click(object sender, EventArgs e)
        {
            send("D");
        }


        private void buttonE_Click(object sender, EventArgs e)
        {
            send("E");
        }

        private void buttonF_Click(object sender, EventArgs e)
        {
            send("F");
        }




        //*****************    EXECUTION    ********************//

        int ra = 0; int fs = 0;

        int rb = 0; int fz = 0;
        int rc = 0;

        int rd = 0; int fc = 0;
        int re = 0; int fac = 0;

        int rh = 0; int fp = 0;
        int rl = 00;




        public int hextod(string hex)
        {

            if (hex != "xx")
            {
                int d = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
                return d;
            }
            else
                return 0;


        }

        public string dtohex(int dec)
        {
            string s = dec.ToString("X");

            return s;
        }




        public int countp(int x)
        {
            if ((dtohex(x).Split('1').Length - 1) % 2 == 1)
                return 0;
            else
                return 1;
        }


        public int pow(int m, int n)
        {
            int r = 0;
            for (int i = 0; i < n; i++)
                r = r * m;

            return r;
        }

        // int min = 750;
        int STK_PNT;
        public void push(string s)
        {
            if (STK_PNT == 0)
            {
                GO_tb.Text = "Please Initialize The Stack !!!";
            }
            if (STK_PNT < 1 || STK_PNT > 999)
                GO_tb.Text = "Invalid Stack Initialization !!!";
            else
                mem[--STK_PNT] = s;
        }

        public string pop()
        {

            if (STK_PNT < 999)
            {
               // DT.Text = "TRUE";
                return (mem[STK_PNT++].ToString());

            }
            else
            {
               // DT.Text = "FALSE";
                return "xx";
            }
        }


        public int ROTL(int val)
        {

            return (val << 1 | val >> (8 - 1));
        }

        public int ROTR(int val)
        {

            return (val >> 1 | val << (8 - 1));
        }

        public void updateflags(int si, int ze, int ca, int au, int pa)
        {

            if (si == 1 & ra < 0)                  //     Flag Sign     Negative
            { fs = 1; }

            if (si == 2 & rb < 0)
            { fs = 1; }

            if (si == 3 & rc < 0)
            { fs = 1; }

            if (si == 4 & rd < 0)
            { fs = 1; }

            if (si == 5 & re < 0)
            { fs = 1; }

            if (si == 6 & rh < 0)
            { fs = 1; }

            if (si == 7 & rl < 0)
            { fs = 1; }

            if (si == 8 & hextod(mem[hextod(dtohex(rh) + dtohex(rl))].ToString()) < 0)
            { fs = 1; }




            if (si == 1 & ra > 0)                  //     Flag Sign      Positive
            { fs = 0; }

            if (si == 2 & rb > 0)
            { fs = 0; }

            if (si == 3 & rc > 0)
            { fs = 0; }

            if (si == 4 & rd > 0)
            { fs = 0; }

            if (si == 5 & re > 0)
            { fs = 0; }

            if (si == 6 & rh > 0)
            { fs = 0; }

            if (si == 7 & rl > 0)
            { fs = 0; }

            if (si == 8 & hextod(mem[hextod(dtohex(rh) + dtohex(rl))].ToString()) > 0)
            { fs = 0; }



            if (ze == 1 & ra == 0)         //          Zero Flag
            {
                fz = 1;
                fs = 0;
                // GO_tb.Text = ra.ToString() + " True";
            }
           // else
              //  GO_tb.Text = ra.ToString() + "False";
                

            if (ze == 2 & rb == 0)
            { fz = 1; fs = 0; }

            if (ze == 3 & rc == 0)
            { fz = 1; fs = 0; }

            if (ze == 4 & rd == 0)
            { fz = 1; fs = 0; }

            if (ze == 5 & re == 0)
            { fz = 1; fs = 0; }

            if (ze == 6 & rh == 0)
            { fz = 1; fs = 0; }

            if (ze == 7 & rl == 0)
            { fz = 1; fs = 0; }

            if (ze == 8 & hextod(mem[hextod(dtohex(rh) + dtohex(rl))].ToString()) == 0)
            { fs = 1; fs = 0; }


            if (ze == 1 & ra != 0)         //          Zero Flag clear
            { fz = 0; }

            if (ze == 2 & rb != 0)
            { fz = 0; }

            if (ze == 3 & rc != 0)
            { fz = 0; }

            if (ze == 4 & rd != 0)
            { fz = 0; }

            if (ze == 5 & re != 0)
            { fz = 0; }

            if (ze == 6 & rh != 0)
            { fz = 0; }

            if (ze == 7 & rl != 0)
            { fz = 0; }

            if (ze == 8 & hextod(mem[hextod(dtohex(rh) + dtohex(rl))].ToString()) != 0)
            { fs = 0; }





            if (ca == 1 & ra > 255)              //       Flag Carry
            {
                ra = ra - 256;
                fc = 1;
            }
            if (ca == 2 & rb > 255)
            {
                rb = rb - 256;
                fc = 1;
            }
            if (ca == 3 & rc > 255)
            {
                rc = rc - 256;
                fc = 1;
            }
            if (ca == 4 & rd > 255)
            {
                rd = rd - 256;
                fc = 1;
            }
            if (ca == 5 & re > 255)
            {
                re = re - 256;
                fc = 1;
            }
            if (ca == 6 & rh > 255)
            {
                rh = rh - 256;
                fc = 1;
            }
            if (ca == 7 & rl > 255)
            {
                rl = rl - 256;
                fc = 1;
            }

            if (ca == 8 & hextod(mem[hextod(dtohex(rh) + dtohex(rl))].ToString()) > 255)
            {
                mem[hextod(dtohex(rh) + dtohex(rl))] = hextod(mem[hextod(dtohex(rh) + dtohex(rl))].ToString()) - 256;
                fc = 1;
            }


            if (au == 1 & ra > 15)             //    Flag Auxilery Carry   
            { fac = 1; }

            if (au == 2 & rb > 15)
            { fac = 1; }

            if (au == 3 & rc > 15)
            { fac = 1; }

            if (au == 4 & rd > 15)
            { fac = 1; }

            if (au == 5 & re > 15)
            { fac = 1; }

            if (au == 6 & rh > 15)
            { fac = 1; }

            if (au == 7 & rl > 15)
            { fac = 1; }

            if (au == 8 & hextod(mem[hextod(dtohex(rh) + dtohex(rl))].ToString()) > 15)
            { fac = 1; }




            if (pa == 1 & countp(ra) == 1)            //     Flag Parity
            { fp = 1; }

            if (pa == 2 & countp(ra) == 1)
            { fp = 1; }

            if (pa == 3 & countp(ra) == 1)
            { fp = 1; }

            if (pa == 4 & countp(ra) == 1)
            { fp = 1; }

            if (pa == 5 & countp(ra) == 1)
            { fp = 1; }

            if (pa == 6 & countp(ra) == 1)
            { fp = 1; }

            if (pa == 7 & countp(ra) == 1)
            { fp = 1; }

            if (au == 8 & countp(hextod(mem[hextod(dtohex(rh) + dtohex(rl))].ToString())) == 0)
            { fp = 1; }



        }

        public void showregs()
        {
            if (ra > 255)
            {
                ra = 255 - ra;
            }

            if (rb > 255)
            {
                rb = 255 - rb;
            }

            if (rc > 255)
            {
                rc = 255 - rc;
            }

            if (rd > 255)
            {
                rd = 255 - rd;
            }

            if (re > 255)
            {
                re = 255 - re;
            }

            if (rh > 255)
            {
                rh = 255 - rh;
            }

            if (rl > 255)
            {
                rl = 255 - rl;
            }

            // Underflow

            if (dtohex(ra).Length > 2)
            {
                ra = hextod(dtohex(ra).Substring(dtohex(ra).Length - 2, 2));
            }

            if (dtohex(rb).Length > 2)
            {
                rb = hextod(dtohex(rb).Substring(dtohex(rb).Length - 2, 2));
            }

            if (dtohex(rc).Length > 2)
            {
                rc = hextod(dtohex(rc).Substring(dtohex(rc).Length - 2, 2));
            }

            if (dtohex(rd).Length > 2)
            {
                rd = hextod(dtohex(rd).Substring(dtohex(rd).Length - 2, 2));
            }

            if (dtohex(re).Length > 2)
            {
                re = hextod(dtohex(re).Substring(dtohex(re).Length - 2, 2));
            }

            if (dtohex(rh).Length > 2)
            {
                rh = hextod(dtohex(rh).Substring(dtohex(rh).Length - 2, 2));
            }

            if (dtohex(rl).Length > 2)
            {
                rl = hextod(dtohex(rl).Substring(dtohex(rl).Length - 2, 2));
            }

            string sra = dtohex(ra);
            string srb = dtohex(rb);
            string src = dtohex(rc);
            string srd = dtohex(rd);
            string sre = dtohex(re);
            string srh = dtohex(rh);
            string srl = dtohex(rl);

            if (sra.Length < 2)
            { RACC.Text = "0" + sra; }
            else
            { RACC.Text = sra; }

            if (srb.Length < 2)
            { RB.Text = "0" + srb; }
            else
            { RB.Text = srb; }

            if (src.Length < 2)
            { RC.Text = "0" + src; }
            else
            { RC.Text = src; }

            if (srd.Length < 2)
            { RD.Text = "0" + srd; }
            else
            { RD.Text = srd; }

            if (sre.Length < 2)
            { RE.Text = "0" + sre; }
            else
            { RE.Text = sre; }

            if (srh.Length < 2)
            { RH.Text = "0" + srh; }
            else
            { RH.Text = srh; }

            if (srl.Length < 2)
            { RL.Text = "0" + srl; }
            else
            { RL.Text = srl; }

            FS.Text = fs.ToString();
            FZ.Text = fz.ToString();
            FC.Text = fc.ToString();
            FAC.Text = fac.ToString();
            FP.Text = fp.ToString();


            string Tex = dtohex(PROG_CNT);
            int lim = Tex.Length;
            lim = 4 - lim;

            for (int i = 0; i < lim; i++)
                Tex = "0" + Tex;


            Program_Counter.Text = Tex;

            string Tex2 = dtohex(STK_PNT);
            int lim2 = Tex2.Length;
            lim2 = 4 - lim2;

            for (int i = 0; i < lim2; i++)
                Tex2 = "0" + Tex2;

            Stack_Pointer.Text = Tex2;

        }


        private void PREV_Click(object sender, EventArgs e)
        {
            if (PROG_CNT > 0)
            {
                ADDR.Text = mem[--PROG_CNT].ToString();
   

                string Tex = dtohex(PROG_CNT);
                int lim = Tex.Length;
                lim = 4 - lim;

                for (int i = 0; i < lim; i++)
                    Tex = "0" + Tex;

                DT.Text = Tex;
            }
        }

        private void NEXT_Click(object sender, EventArgs e)
        {
            if(mem[PROG_CNT]==null)
               return;


            if (PROG_CNT < 1000)
                {
                // PROG_CNT++;
                // ADDR.Text = PROG_CNT.ToString();
                   ADDR.Text = mem[++PROG_CNT].ToString();
                string Tex = dtohex(PROG_CNT);
                   int lim =Tex.Length;
                   lim = 4 - lim;

                   for (int i = 0; i < lim; i++)
                      Tex = "0" + Tex;


                     DT.Text = Tex;
                }

        }

        private void REG_Click(object sender, EventArgs e)
        {
            showregs();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ra = 0; fs = 0;

            rb = 0; fz = 0;
            rc = 0;

            rd = 0; fc = 0;
            re = 0; fac = 0;

            rh = 0; fp = 0;
            rl = 00;

            showregs();

            tstate = 0;
            Mcycle = 0;
           
            bytes = 0;


            TSbox.Text = tstate.ToString();
            MCbox.Text = Mcycle.ToString();
            Bytebox.Text = bytes.ToString();
            Timebox.Text = (Mcycle * 2).ToString();
        }

        private void GO_Click(object sender, EventArgs e)
        {
            string text = GO_tb.Text;

            for (int i = 0; i < (4 - text.Length); i++)
                text = "0" + text;


            text = text.Substring(0, 4);

            bool err;//= text.All(char.IsDigit);

            //    for (int i = 0; i < 3; i++)
            //      if (!text.All(char.IsDigit) || text.Substring(i, 1) != "A" || text.Substring(i, 1) != "B" || text.Substring(i, 1) != "C" || text.Substring(i, 1) != "D" || text.Substring(i, 1) != "E" || text.Substring(i, 1) != "F")
            //        err = false;

            err=System.Text.RegularExpressions.Regex.IsMatch(text, @"\A\b[0-9a-fA-F]+\b\Z");


            if (!err)
            {
                GO_tb.Text = "Wrong Address !!!";
            }

            else 
            {
                if (hextod(text) >= 0 & hextod(text) < 999)
                {

                    PROG_CNT = hextod(GO_tb.Text);

                   string ttxt  = dtohex(PROG_CNT);
                    for (int i = 0; i < 4 - ttxt.Length; i++)
                        ttxt = "0" + ttxt;
                    DT.Text = ttxt;

                    if (mem[PROG_CNT] == null)
                    {
                        ADDR.Text = "xx";
                        return;
                    }

                    string ss = mem[PROG_CNT].ToString();
                    if (ss.Length == 1)
                        ss = "0" + ss;
                }
                else
                   GO_tb.Text = "Address is not within range !!!";
            }

        }

        private void Back_Click(object sender, EventArgs e)
        {

            MainMenu back = new MainMenu();
            back.Show();

        }

        private void Del_Click(object sender, EventArgs e)
        {
            temp = null;
            ADDR.Text = "";
        }



        private void STEP_Click(object sender, EventArgs e)
        {

            if (mem[PROG_CNT] == null)
            {
                GO_tb.Text = "Please Start with address 0";
                return;
            }

            string test = mem[PROG_CNT].ToString();
            while (mem[PROG_CNT].ToString() != "xx")
            {
                // MVI Instruction //

                if (mem[PROG_CNT].ToString() == "3E")                 //  MVI A,DATA
                {

                    PROG_CNT = PROG_CNT + 1;
                    string t = mem[PROG_CNT].ToString();
                    ra = hextod(t);
                    PROG_CNT = PROG_CNT + 1;
                  
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "06")                  //  MVI B,DATA
                {
                    rb = hextod(mem[++PROG_CNT].ToString());
                    PROG_CNT = PROG_CNT + 1;
                   
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "0E")                   //  MVI C,DATA
                {
                    rc = hextod(mem[++PROG_CNT].ToString());
                    PROG_CNT = PROG_CNT + 1;
             
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "16")                   //  MVI D,DATA
                {
                    rd = hextod(mem[++PROG_CNT].ToString());
                    PROG_CNT = PROG_CNT + 1;
          
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "1E")                   //   MVI E,DATA
                {
                    re = hextod(mem[++PROG_CNT].ToString());
                    PROG_CNT = PROG_CNT + 1;
                   
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "26")                    //   MVI H,DATA
                {
                    rh = hextod(mem[++PROG_CNT].ToString());
                    PROG_CNT = PROG_CNT + 1;
                   
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "2E")                    //   MVI L,DATA
                {
                    rl = hextod(mem[++PROG_CNT].ToString());
                    PROG_CNT = PROG_CNT + 1;
                   
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "36")                    //    MVI M[HL],DATA
                {
                    //mem[hextod(dtohex(rh) + dtohex(rl))] = mem[++PROG_CNT];

                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);
                   
                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    mem[hextod(s1 + s2)] = mem[++PROG_CNT];

                    PROG_CNT = PROG_CNT + 1;
                   
                    Mcycle += 3;
                    tstate += 13;
                    showregs();
                    break;
                }


                // LXI Instruction

                if (mem[PROG_CNT].ToString() == "01")                 //  LXI  BC , MEM
                {
                    string t1 = mem[++PROG_CNT].ToString();
                    rc = hextod(t1);
                    string t2 = mem[++PROG_CNT].ToString();
                    rb = hextod(t2);

                    PROG_CNT = PROG_CNT + 1;
                 
                    Mcycle += 3;
                    tstate += 10;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "11")                 //  LXI  DE , MEM
                {
                    string t1 = mem[++PROG_CNT].ToString();
                    re = hextod(t1);
                    string t2 = mem[++PROG_CNT].ToString();
                    rd = hextod(t2);

                    PROG_CNT = PROG_CNT + 1;
                    
                    Mcycle += 3;
                    tstate += 10;
                    showregs();
                    break;

                }

                if (mem[PROG_CNT].ToString() == "21")                 //  LXI  HL , MEM
                {
                    string t1 = mem[++PROG_CNT].ToString();
                    rl = hextod(t1);
                    string t2 = mem[++PROG_CNT].ToString();
                    rh = hextod(t2);

                    PROG_CNT = PROG_CNT + 1;
                  
                    Mcycle += 3;
                    tstate += 10;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "31")                 //  LXI  SP , MEM
                {

                    string t = mem[++PROG_CNT].ToString();
                    t = mem[++PROG_CNT].ToString() + t;
                    STK_PNT = (hextod(t));

                    PROG_CNT++;
                  
                    Mcycle += 3;
                    tstate += 10;
                    showregs();
                    break;
                }


                //  MOV Instruction  //                        
                if (mem[PROG_CNT].ToString() == "7F")                   //   MOV A,A
                {
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "78")                   //   MOV A,B
                {
                    ra = rb;
                    PROG_CNT = PROG_CNT + 1;
                 
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "79")                    //   MOV A,C
                {
                    ra = rc;
                    PROG_CNT = PROG_CNT + 1;
                   
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "7A")                     //   MOV A,D
                {
                    ra = rd;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "7B")                     //    MOV A,E
                {
                    ra = re;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "7C")                      //    MOV A,H
                {
                    ra = rh;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "7D")                      //    MOV A,L
                {
                    ra = rl;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "7E")                      //     MOV A,M[HL]
                {
                    
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    ra = hextod(mem[hextod(s1+s2)].ToString());
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 2;
                    tstate += 7;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "47")                      //    MOV B,A
                {
                    rb = ra;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "40")                      //    MOV B,B
                {
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "41")                      //    MOV B,C
                {
                    rb = rc;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "42")                      //    MOV B,D
                {
                    rb = rd;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "43")                      //    MOV B,E
                {
                    rb = re;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "44")                      //    MOV B,H
                {
                    rb = rh;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "45")                      //    MOV B,L
                {
                    rb = rl;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "46")                      //    MOV B,M[HL]
                {
                    
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    rb = hextod(mem[hextod(s1 + s2)].ToString());
                 
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 2;
                    tstate += 7;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "4F")                      //    MOV C,A
                {
                    rc = ra;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "48")                      //    MOV C,B
                {
                    rc = rb;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "49")                      //    MOV C,C
                {
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "4A")                      //    MOV C,D
                {
                    rc = rd;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "4B")                      //    MOV C,E
                {
                    rc = re;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "4C")                      //    MOV C,H
                {
                    rc = rh;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "4D")                      //    MOV86
                {
                    rc = rl;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "4E")                      //    MOV C,M[HL]
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;


                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    rc = hextod(mem[hextod(s1 + s2)].ToString());

                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 2;
                    tstate += 7;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "57")                      //    MOV D,A
                {
                    rd = ra;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "50")                      //    MOV D,B
                {
                    rd = rb;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "51")                      //    MOV D,C
                {
                    rd = rc;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "52")                      //    MOV D,D
                {
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "53")                      //    MOV D,E
                {
                    rd = re;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "54")                      //    MOV D,H
                {
                    rd = rh;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "55")                      //    MOV D,L
                {
                    rd = rl;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "56")                      //    MOV D,M[HL]
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;


                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    rd = hextod(mem[hextod(s1 + s2)].ToString());

                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 2;
                    tstate += 7;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "5F")                      //    MOV E,A
                {
                    re = ra;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "58")                      //    MOV E,B
                {
                    re = rb;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "59")                      //    MOV E,C
                {
                    re = rc;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "5A")                      //    MOV E,D
                {
                    re = rd;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "5B")                      //    MOV E,E
                {
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "5C")                      //    MOV E,H
                {
                    re = rh;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "5D")                      //    MOV E,L
                {
                    re = rl;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "5E")                      //    MOV E,M[HL]
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;


                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    re = hextod(mem[hextod(s1 + s2)].ToString());

                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 2;
                    tstate += 7;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "67")                      //    MOV H,A
                {
                    rh = ra;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "60")                      //    MOV H,B
                {
                    rh = rb;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "61")                      //    MOV H,C
                {
                    rh = rc;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "62")                      //    MOV H,D
                {
                    rh = rd;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "63")                      //    MOV H,E
                {
                    rh = re;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "64")                      //    MOV H,H
                {
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "65")                      //    MOV H,L
                {
                    rh = rl;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "5E")                      //    MOV H,M[HL]
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;


                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    rh = hextod(mem[hextod(s1 + s2)].ToString());

                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 2;
                    tstate += 7;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "6F")                      //    MOV L,A
                {
                    rl = ra;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "68")                      //    MOV L,B
                {
                    rl = rb;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "69")                      //    MOV L,C
                {
                    rl = rc;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "6A")                      //    MOV L,D
                {
                    rl = rd;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "6B")                      //    MOV L,E
                {
                    rl = re;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "6C")                      //    MOV L,H
                {
                    rl = rh;
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "6D")                      //    MOV L,L
                {
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "6E")                      //    MOV L,M[HL]
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;


                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    rl = hextod(mem[hextod(s1 + s2)].ToString());

                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 2;
                    tstate += 7;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "77")                       //   MOV M[HL],A
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;


                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    mem[hextod(s1 + s2)] = dtohex(ra);
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;

                }

                if (mem[PROG_CNT].ToString() == "70")                       //   MOV M[HL],B
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;


                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    mem[hextod(s1 + s2)] = dtohex(rb);
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "71")                       //   MOV M[HL],C
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    mem[hextod(s1 + s2)] = dtohex(rc);
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "72")                       //   MOV M[HL],D
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    mem[hextod(s1 + s2)] = dtohex(rd);
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "73")                       //   MOV M[HL],E
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    mem[hextod(s1 + s2)] = dtohex(re);
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "74")                       //   MOV M[HL],H
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    mem[hextod(s1 + s2)] = dtohex(rh);
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "75")                       //   MOV M[HL],L
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    mem[hextod(s1 + s2)] = dtohex(rl);
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }


                //    INR Instructions   //


                if (mem[PROG_CNT].ToString() == "3C")           // INR A
                {
                    ra++;
                    PROG_CNT++;
                    updateflags(1, 1, 0, 1, 1);
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "04")           // INR B
                {
                    rb++;
                    PROG_CNT++;
                    updateflags(2, 2, 0, 2, 2);
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "0C")           // INR C
                {
                    rc++;
                    PROG_CNT++;
                    updateflags(3, 3, 0, 3, 3);
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "14")           // INR D
                {
                    rd++;
                    PROG_CNT++;
                    updateflags(4, 4, 0, 4, 4);
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "1C")           // INR E
                {
                    re++;
                    PROG_CNT++;
                    updateflags(5, 5, 0, 5, 5);
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "24")           // INR H
                {
                    rh++;
                    PROG_CNT++;
                    updateflags(6, 6, 0, 6, 6);
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "2C")           // INR L
                {
                    rl++;
                    PROG_CNT++;
                    updateflags(7, 7, 0, 7, 7);
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "34")           // INR M[HL]
                {
                   
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;


                    if (hextod(s1 + s2) > 999 || hextod(s1 + s2) < 0)
                    {
                        GO_tb.Text = "Wrong Address given !!!";
                        break;
                    }

                    int t = hextod(mem[hextod(s1 + s2)].ToString());
                 
                    mem[hextod(s1+s2)] = dtohex(++t);
                    updateflags(8, 8, 0, 8, 8);
                  
                    PROG_CNT++;
                    Mcycle += 3;
                    tstate += 10;
                    showregs();
                    break;

                }

                if (mem[PROG_CNT].ToString() == "03")           // INX BC
                {

                    string s1 = dtohex(rb);
                    string s2 = dtohex(rc);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    int val = hextod(s1 + s2) + 1;

                    string vals = dtohex(val);
                    int len = vals.Length;

                    if (len == 3)
                        vals = "0" + vals;
                    else if (len == 2)
                        vals = "00" + vals;
                    else if (len == 1)
                        vals = "000" + vals;

                    int len2 = vals.Length;

                    rc = hextod(vals.Substring(2, 2));
                    rb = hextod(vals.Substring(0, 2));
                   
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 6;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "13")           // INX DE
                {
                    string s1 = dtohex(rd);
                    string s2 = dtohex(re);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    int val = hextod(s1 + s2) + 1;

                    string vals = dtohex(val);
                    int len = vals.Length;

                    if (len == 3)
                        vals = "0" + vals;
                    else if (len == 2)
                        vals = "00" + vals;
                    else if (len == 1)
                        vals = "000" + vals;

                    int len2 = vals.Length;

                    re = hextod(vals.Substring(2, 2));
                    rd = hextod(vals.Substring(0, 2));
                   
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 6;
                    showregs();
                    break;

                }

                if (mem[PROG_CNT].ToString() == "23")           // INX HL
                {

                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    int val = hextod(s1 + s2) + 1;

                    string vals = dtohex(val);
                    int len = vals.Length;

                    if (len == 3)
                        vals = "0" + vals;
                    else if (len == 2)
                        vals = "00" + vals;
                    else if (len == 1)
                        vals = "000" + vals;

                    int len2 = vals.Length;

                    rl = hextod(vals.Substring(2, 2));
                    rh = hextod(vals.Substring(0, 2));
                  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 6;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "33")           // INX SP
                {
                    STK_PNT++;
                    PROG_CNT++;
                    Mcycle += 1;
                    Stack_Pointer.Text = dtohex(STK_PNT);

                    tstate += 6;
                    showregs();
                    break;
                }



                // DCR Instructions//
                if (mem[PROG_CNT].ToString() == "3D")           // DCR A
                {
                    ra--;
                    updateflags(1, 1, 0, 1, 1);
                    PROG_CNT++;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "05")           // DCR B
                {
                    rb--;
                    updateflags(2, 2, 0, 2, 2);
                    PROG_CNT++;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "0D")           // DCR C
                {
                    rc--;
                    updateflags(3, 3, 0, 3, 3);
                    PROG_CNT++;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "15")           // DCR D
                {
                    rd--;
                    updateflags(4, 4, 0, 4, 4);
                    PROG_CNT++;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "1D")           // DCR E
                {
                    re--;
                    updateflags(5, 5, 0, 5, 5);
                    PROG_CNT++;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "25")           // DCR H
                {
                    rh--;
                    updateflags(6, 6, 0, 6, 6);
                    PROG_CNT++;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "2D")           // DCR L
                {
                    rl--;
                    updateflags(7, 7, 0, 7, 7);
                    PROG_CNT++;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }
                if (mem[PROG_CNT].ToString() == "35")           // DCR M[HL]
                {

                    string s1 = dtohex(rb);
                    string s2 = dtohex(rc);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;



                    int val = hextod(s1 + s2) - 1;

                    string vals = dtohex(val);
                    int len = vals.Length;

                    if (len == 3)
                        vals = "0" + vals;
                    else if (len == 2)
                        vals = "00" + vals;
                    else if (len == 1)
                        vals = "000" + vals;

                    int len2 = vals.Length;

                    rc = hextod(vals.Substring(2, 2));
                    rb = hextod(vals.Substring(0, 2));
                   
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 6;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "0B")           // DCX BC
                {

                    string s1 = dtohex(rb);
                    string s2 = dtohex(rc);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    int val = hextod(s1 + s2) - 1;

                    string vals = dtohex(val);
                    int len = vals.Length;

                    if (len == 3)
                        vals = "0" + vals;
                    else if (len == 2)
                        vals = "00" + vals;
                    else if (len == 1)
                        vals = "000" + vals;

                    int len2 = vals.Length;

                    rc = hextod(vals.Substring(2, 2));
                    rb = hextod(vals.Substring(0, 2));
                  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 6;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "1B")           // DCX DE
                {

                    string s1 = dtohex(rd);
                    string s2 = dtohex(re);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    int val = hextod(s1 + s2) - 1;

                    string vals = dtohex(val);
                    int len = vals.Length;

                    if (len == 3)
                        vals = "0" + vals;
                    else if (len == 2)
                        vals = "00" + vals;
                    else if (len == 1)
                        vals = "000" + vals;

                    int len2 = vals.Length;

                    re = hextod(vals.Substring(2, 2));
                    rd = hextod(vals.Substring(0, 2));
              
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 6;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "2B")           // DCX HL
                {

                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    int val = hextod(s1 + s2) - 1;

                    string vals = dtohex(val);
                    int len = vals.Length;

                    if (len == 3)
                        vals = "0" + vals;
                    else if (len == 2)
                        vals = "00" + vals;
                    else if (len == 1)
                        vals = "000" + vals;

                    int len2 = vals.Length;

                    rl = hextod(vals.Substring(2, 2));
                    rh = hextod(vals.Substring(0, 2));
                  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 6;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "3B")           // DCX SP
                {

                    STK_PNT--;
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 6;
                    showregs();
                    break;

                }



                //LDA Instruction//
                if (mem[PROG_CNT].ToString() == "3A")           // LDA
                {
                    string h, l, t;
                    l = mem[++PROG_CNT].ToString();
                    h = mem[++PROG_CNT].ToString();
                    t = h + l;

                    int va = hextod(t);

                    if (hextod(t) > 999 || hextod(t) < 0)
                    {
                        GO_tb.Text = "Wrong Address Given !!!";
                        break;
                    }

                    ra = hextod(mem[va].ToString());
                  
                    PROG_CNT++;
                    Mcycle += 3;
                    tstate += 10;
                    showregs();
                    break;
                }



                //  ADI instruction //                             
                if (mem[PROG_CNT].ToString() == "C6")                  //  ADI DATA
                {
                  //  GO_tb.Text = ra.ToString();


                    ra += hextod(mem[++PROG_CNT].ToString());
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;

                   // GO_tb.Text += " "+ra.ToString();

                    showregs();
                    Mcycle += 2;
                    tstate += 7;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "CE")                  //  ACI DATA
                {
                    ra += hextod(mem[++PROG_CNT].ToString());
                    ra += fc;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 2;
                    tstate += 7;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "8F")                  //  ADC A
                {
                    ra = ra + ra + fc;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "88")                  //  ADC B
                {
                    ra = ra + rb + fc;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "89")                  //  ADC C
                {
                    ra = ra + rc + fc;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "8A")                  //  ADC D
                {
                    ra = ra + rd + fc;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "8B")                  //  ADC E
                {
                    ra = ra + re + fc;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "8C")                  //  ADC H
                {
                    ra = ra + rh + fc;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "8D")                  //  ADC L
                {
                    ra = ra + rl + fc;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "8E")                  //  ADC M
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    int i69 = hextod(s1 + s2);
                    string s108 = mem[i69].ToString();
                    ra = ra + fc + hextod(s108);
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT++;
                    showregs();
                    Mcycle += 2;
                    tstate += 7;
                    break;
                }


                if (mem[PROG_CNT].ToString() == "87")                  //  ADD A
                {
                    ra = ra + ra;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }



                if (mem[PROG_CNT].ToString() == "80")                  //  ADD B
                {

                    ra = ra + rb;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "81")                  //  ADD C
                {
                    ra = ra + rc;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "82")                  //  ADD D
                {
                    ra = ra + rd;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "83")                  //  ADD E
                {
                    ra = ra + re;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT = PROG_CNT + 1;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "84")                  //  ADD H
                {
                    ra = ra + rh;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT++;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "85")                  //  ADD L
                {
                    ra = ra + rl;
                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT++;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "86")                  //  ADD M
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    int i69 = hextod(s1+s2);
                    string s108 = mem[i69].ToString();

                    ra = ra + hextod(s108);

                    updateflags(1, 1, 1, 1, 1);
                    PROG_CNT++;
                    showregs();
                    Mcycle += 2;
                    tstate += 7;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "A7")                    //  ANA A
                {
                    ra = ra & ra;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 1;
                    fac = 0;
                 
                    PROG_CNT++;

                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "A0")                    //  ANA B
                {
                    ra = ra & rb;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 1;
                  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "A1")                    //  ANA C
                {
                    ra = ra & rc;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 1;
                   
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "A2")                    //  ANA D
                {
                    ra = ra & rd;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 1;
                
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "A3")                    //  ANA E
                {
                    ra = ra & re;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 1;
                   
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "A4")                    //  ANA H
                {
                    ra = ra & rh;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 1;
                 
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "A5")                    //  ANA L
                {
                    ra = ra & rl;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 1;
               
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "A6")                    //  ANA M[HL]
                {

                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    ra = ra & hextod(mem[hextod(s1+s2)].ToString());
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 1;
                   
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "E6")                    //  ANI DATA
                {
                    ra = ra & hextod(mem[++PROG_CNT].ToString());
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 1;
                   
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "00")                      //   NOP
                {
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "B7")                       //  ORA A
                {
                    ra = ra | ra;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                 
                    PROG_CNT++;
                    showregs();
                    Mcycle += 1;
                    tstate += 4;
                    break;
                }

                if (mem[PROG_CNT].ToString() == "B0")                       //  ORA B
                {
                    ra = ra | rb;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "B1")                       //  ORA C
                {
                    ra = ra | rc;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
               
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "B2")                       //  ORA D
                {
                    ra = ra | rd;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                   
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "B3")                       //  ORA E
                {
                    ra = ra | re;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                    
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "B4")                       //  ORA H
                {
                    ra = ra | rh;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "B5")                       //  ORA L
                {
                    ra = ra | rl;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;

                }

                if (mem[PROG_CNT].ToString() == "B6")                       //  ORA M[HL]
                {

                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    ra = ra | hextod(mem[hextod(s1+s2)].ToString());
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
               
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "F6")                       //  ORA DATA
                {
                    ra = ra | hextod(mem[++PROG_CNT].ToString());
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                   
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "AF")                        //   XRA A
                {
                    ra = ra ^ ra;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }
                
                if (mem[PROG_CNT].ToString() == "A8")                        //   XRA B
                {
                    ra = ra ^ rb;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                    
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "A9")                        //   XRA C
                {
                    ra = ra ^ rc;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                    
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "AA")                        //   XRA D
                {
                    ra = ra ^ rd;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                   
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "AB")                        //   XRA E
                {
                    ra = ra ^ re;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "AC")                        //   XRA H
                {
                    ra = ra ^ rh;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "AD")                        //   XRA L
                {
                    ra = ra ^ rl;
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                 
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "AE")                        //   XRA M[HL]
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    ra = ra ^ hextod(mem[hextod(s1 + s2)].ToString());
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                   
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "EE")                        //   XRI DATA
                {
                    ra = ra ^ hextod(mem[++PROG_CNT].ToString());
                    updateflags(1, 1, 1, 1, 1);
                    fc = 0;
                    fac = 0;
                 
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "9F")                        //   SBB A;
                {
                    ra = ra - ra - fc;
                    updateflags(1, 1, 1, 1, 1);
                  
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "98")                        //   SBB B;
                {
                    ra = ra - rb - fc;
                    updateflags(1, 1, 1, 1, 1);
                   
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "99")                        //   SBB C;
                {
                    ra = ra - rc - fc;
                    updateflags(1, 1, 1, 1, 1);
                   
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "9A")                        //   SBB D;
                {
                    ra = ra - rd - fc;
                    updateflags(1, 1, 1, 1, 1);
                 
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "9B")                        //   SBB E;
                {
                    ra = ra - re - fc;
                    updateflags(1, 1, 1, 1, 1);
                  
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "9C")                        //   SBB H;
                {
                    ra = ra - rh - fc;
                    updateflags(1, 1, 1, 1, 1);
                  
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "9D")                        //   SBB L;
                {
                    ra = ra - rl - fc;
                    updateflags(1, 1, 1, 1, 1);
                   
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "9E")                        //   SBB M[HL];
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    ra = ra - hextod(mem[hextod(s1 + s2)].ToString()) - fc;
                    updateflags(1, 1, 1, 1, 1);
                   
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "DE")                        //   SBB DATA;
                {
                    ra = ra - hextod(mem[++PROG_CNT].ToString()) - fc;
                    updateflags(1, 1, 1, 1, 1);
                    
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "97")                        //   SUB A;
                {
                    ra = ra - ra;
                    updateflags(1, 1, 1, 1, 1);
                  
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "90")                        //   SUB B;
                {
                    ra = ra - rb;
                    updateflags(1, 1, 1, 1, 1);
                   
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "91")                        //   SUB C;
                {
                    ra = ra - rc;
                    updateflags(1, 1, 1, 1, 1);
                   
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "92")                        //   SUB D;
                {
                    ra = ra - rd;
                    updateflags(1, 1, 1, 1, 1);
                   
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "93")                        //   SUB E;
                {
                    ra = ra - re;
                    updateflags(1, 1, 1, 1, 1);
                    
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "94")                        //   SUB H;
                {
                    ra = ra - rh;
                    updateflags(1, 1, 1, 1, 1);
                   
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "95")                        //   SUB L;
                {
                    ra = ra - rl;
                    updateflags(1, 1, 1, 1, 1);
                   
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "96")                        //   SUB M[HL];
                {
                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    ra = ra - hextod(mem[hextod(s1 + s2)].ToString());
                    updateflags(1, 1, 1, 1, 1);
                   
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "D6")                        //   SUI DATA;
                {

                    ra = ra - hextod(mem[++PROG_CNT].ToString());
                    updateflags(1, 1, 1, 1, 1);
                  
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "27")                        //   DAA
                {
                    RACC.Text = hextod(dtohex(ra)).ToString();
                    updateflags(1, 1, 1, 1, 1);
                    
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 10;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "09")                        //   DAD B
                {
                    rl = rl + rc;
                    rh = rh + rb;
                    updateflags(0, 0, 7, 0, 0);

                    if (fc == 1)
                        rh += 1;
                    showregs();

                  
                    PROG_CNT++;
                    Mcycle += 3;
                    tstate += 10;
                    showregs();
                    break;

                }

                if (mem[PROG_CNT].ToString() == "19")                        //   DAD D
                {
                    rl = rl + re;
                    rh = rh + rd;
                    updateflags(0, 0, 7, 0, 0);
                    if (fc == 1)
                        rh += 1;
                    showregs();
                    updateflags(0, 0, 7, 0, 0);
                   
                    PROG_CNT++;
                    Mcycle += 3;
                    tstate += 10;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "29")                        //   DAD H
                {
                    rl = rl + rl;
                    rh = rh + rh;
                    updateflags(0, 0, 7, 0, 0);
                    if (fc == 1)
                        rh += 1;
                    showregs();
                    updateflags(0, 0, 7, 0, 0);
                   
                    PROG_CNT++;
                    Mcycle += 3;
                    tstate += 10;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "39")                        //   DAD SP
                {
                    rl = rl + rl;
                    rh = rh + rh;
                    updateflags(0, 0, 7, 0, 0);
                    if (fc == 1)
                        rh += 1;
                    showregs();
                    updateflags(0, 0, 7, 0, 0);
                   
                    Mcycle += 3;
                    PROG_CNT++;
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "0A")                        //   LDAX M[BC];
                {
                    string s1 = dtohex(rb);
                    string s2 = dtohex(rc);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    ra = hextod(mem[hextod(s1+s2)].ToString());
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "1A")                        //   LDAX M[DE];
                {
                    string s1 = dtohex(rd);
                    string s2 = dtohex(re);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    ra = hextod(mem[hextod(s1 + s2)].ToString());
                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "2A")                        //   LHLD ppqq;
                {
                    string p = mem[++PROG_CNT].ToString();

                    rl = hextod(p);

                    p = mem[++PROG_CNT].ToString();

                    rh = hextod(p);

                    showregs();
                   

                    PROG_CNT = PROG_CNT + 1;
                    Mcycle += 5;
                    tstate += 16;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "22")                         //   SHLD ppqq
                {
                    string st = mem[++PROG_CNT].ToString();
                    st = mem[++PROG_CNT].ToString() + st;

                    mem[hextod(st)] = dtohex(rl);
                    mem[hextod(st) + 1] = dtohex(rh);
                    PROG_CNT++;
                    Mcycle += 5;
                    tstate += 16;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "F9")                         //     SPHL
                {
                    STK_PNT = hextod(dtohex(rh) + dtohex(rl));
                    Stack_Pointer.Text = dtohex(STK_PNT);
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 6;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "32")                         //     STA ppqq
                {
                    string st = mem[++PROG_CNT].ToString();
                    st = mem[++PROG_CNT].ToString() + st;

                    string dt = dtohex(ra);

                    if (dt.Length == 1)
                        dt = "0" + dt;

                    if (hextod(st) > 999 || hextod(st) < 0)
                    {
                        GO_tb.Text = "Wrong Address Given !!!";
                        break;
                    }

                    mem[hextod(st)] = dt;
                    PROG_CNT++;
                    Mcycle += 4;
                    tstate += 13;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "02")                         //     STAX B
                {
                    mem[hextod(dtohex(rb) + dtohex(rc))] = dtohex(ra);
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "12")                         //     STAX D
                {
                    mem[hextod(dtohex(rd) + dtohex(re))] = dtohex(ra);
                    PROG_CNT++;
                    Mcycle += 2;
                    tstate += 7;
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "EB")                         //     XCNG
                {
                    int w, z;
                    w = rh;
                    z = rl;

                    rh = rd;
                    rl = re;

                    rd = w;
                    re = z;

                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "E3")                         //      XTHL
                {
                    int a, b;

                    a = hextod(mem[STK_PNT].ToString());
                    b = hextod(mem[STK_PNT + 1].ToString());

                    mem[STK_PNT] = rl;
                    mem[STK_PNT + 1] = rh;

                    rl = a;
                    rh = b;

                    PROG_CNT++;
                    Mcycle += 5;
                    tstate += 16;
                    showregs();
                    break;

                }



                if (mem[PROG_CNT].ToString() == "37")                        //     STC
                {
                    fc = 1;
                   
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "2F")                       //      CMA A
                {

                    // ra = 10;

                    //  ra = ~ra;
                    string tem = Convert.ToString(ra, 2);
                    string res = null;

                    int lim = tem.Length;
                    lim = 8 - lim;

                    for (int i = 0; i < lim; i++)
                        tem = "0" + tem;

                    for (int i = 0; i <= 7; i++)
                    {

                        if (tem.Substring(i, 1) == "1")
                            res = res + "0";
                        else
                            res = res + "1";

                    }

                    ra = Convert.ToInt32(res, 2);
  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "3F")                       //      CMC 
                {
                    if (fc == 1)
                        fc = 0;
                    else
                        fc = 1;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "BF")                        //     CMP A
                {
                    int tra = ra;
                    ra = ra - ra;

                    updateflags(1, 1, 1, 1, 1);
                    ra = tra;

                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "B8")                        //     CMP B
                {
                    int tra = ra;
                    ra = ra - rb;

                    updateflags(1, 1, 1, 1, 1);
                    ra = tra;
                  
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "B9")                        //     CMP C
                {
                    int tra = ra;
                    ra = ra - rc;

                    updateflags(1, 1, 1, 1, 1);
                    ra = tra;
             
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "BA")                        //     CMP D
                {
                    int tra = ra;
                    ra = ra - rd;

                    updateflags(1, 1, 1, 1, 1);
                    ra = tra;
             
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "BB")                        //     CMP E
                {
                    int tra = ra;
                    ra = ra - re;

                    updateflags(1, 1, 1, 1, 1);
                    ra = tra;
                
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "BC")                        //     CMP H
                {
                    int tra = ra;
                    ra = ra - rh;

                    updateflags(1, 1, 1, 1, 1);
                    ra = tra;
                   
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "BD")                        //     CMP L
                {
                    int tra = ra;
                    ra = ra - rl;

                    updateflags(1, 1, 1, 1, 1);
                    ra = tra;
                 
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "BE")                        //     CMP M
                {

                    int tra = ra;
                    ra = ra - hextod(mem[hextod(dtohex(rh) + dtohex(rl))].ToString());

                    updateflags(1, 1, 1, 1, 1);
                    ra = tra;
                
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 7;
                    showregs();
                    break;
                }


                //                         Brunch Instructions                           //


                if (mem[PROG_CNT].ToString() == "DA")           // JC ppqq
                {

                    if (fc == 1)
                    {
                        string jstr = mem[++PROG_CNT].ToString();
                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);
                        Mcycle += 3;
                        tstate += 10;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "FA")           // JM ppqq
                {

                    if (fs == 1)
                    {
                        string jstr = mem[++PROG_CNT].ToString();
                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);
                        Mcycle += 3;
                        tstate += 10;
                    }
                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "C3")           // JMP ppqq
                {

                    string jstr = mem[++PROG_CNT].ToString();
                    jstr = mem[++PROG_CNT].ToString() + jstr;
                    PROG_CNT = hextod(jstr);
                    Mcycle += 3;
                    tstate += 10;
                    showregs();
                    break;

                }


                if (mem[PROG_CNT].ToString() == "D2")           // JNC ppqq
                {

                    if (fc == 0)
                    {
                        string jstr = mem[++PROG_CNT].ToString();
                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);
                        Mcycle += 3;
                        tstate += 10;
                    }
                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "C2")           // JNZ ppqq
                {

                    if (fz == 0)
                    {
                        string jstr = mem[++PROG_CNT].ToString();
                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);
                        Mcycle += 3;
                        tstate += 10;
                    }
                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "F2")           // JP ppqq
                {

                    if (fs == 0)
                    {
                        string jstr = mem[++PROG_CNT].ToString();
                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);
                        Mcycle += 3;
                        tstate += 10;
                    }
                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "EA")           // JPE ppqq
                {

                    if (fp == 1)
                    {
                        string jstr = mem[++PROG_CNT].ToString();
                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);
                        Mcycle += 3;
                        tstate += 10;
                    }
                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "E2")           // JPO ppqq
                {

                    if (fp == 0)
                    {
                        string jstr = mem[++PROG_CNT].ToString();
                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);
                        Mcycle += 3;
                        tstate += 10;
                    }
                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "CA")           // JZ ppqq
                {

                    if (fz == 1)
                    {
                        string jstr = mem[++PROG_CNT].ToString();
                        jstr = mem[++PROG_CNT].ToString() + jstr;

                        PROG_CNT = hextod(jstr);
                        Mcycle += 3;
                        tstate += 10;
                    }
                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }

                //            Rotation             //



                if (mem[PROG_CNT].ToString() == "17")            //  RAL  
                {


                    string s = Convert.ToString(ra, 2);

                    int lim = s.Length;
                    lim = 8 - lim;

                    for (int i = 0; i < lim; i++)
                        s = "0" + s;

                    string t = s.Substring(0, 1);
                    s = s.Substring(1, 7) + dtohex(fc);

                    fc = hextod(t);

                    ra = Convert.ToInt32(s, 2);
           
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;

                    showregs();
                    break;

                }


                if (mem[PROG_CNT].ToString() == "1F")            //  RAR  
                {

                    string s = Convert.ToString(ra, 2);


                    int lim = s.Length;
                    lim = 8 - lim;

                    for (int i = 0; i < lim; i++)
                        s = "0" + s;

                    string t = s.Substring(7, 1);

                    s = dtohex(fc) + s.Substring(0, 7);

                    fc = hextod(t);

                    ra = Convert.ToInt32(s, 2);
                   
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;

                    showregs();
                    break;

                }



                if (mem[PROG_CNT].ToString() == "07")            //  RLC  
                {

                    string s = Convert.ToString(ra, 2);

                    int lim = s.Length;
                    lim = 8 - lim;

                    for (int i = 0; i < lim; i++)
                        s = "0" + s;

                    string t = s.Substring(0, 1);

                    s = s.Substring(1, 7) + t;


                    fc = hextod(t);

                    ra = Convert.ToInt32(s, 2);
           
                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;

                    showregs();
                    break;
                }



                if (mem[PROG_CNT].ToString() == "0F")            //  RRC 
                {


                    string s = Convert.ToString(ra, 2);

                    int lim = s.Length;
                    lim = 8 - lim;

                    for (int i = 0; i < lim; i++)
                        s = "0" + s;


                    string t = s.Substring(7, 1);

                    s = t + s.Substring(0, 7);


                    fc = hextod(t);

                    ra = Convert.ToInt32(s, 2);

                    PROG_CNT++;
                    Mcycle += 1;
                    tstate += 4;

                    showregs();
                    break;

                }




                //     Intrupts     //

                if (mem[PROG_CNT].ToString() == "C7")                   //    RST 0
                {
                    tstate += 12;
                    Mcycle += 3;

                    bytes = 0;
                    for (int i = 0; i < 999; i++)
                        if (mem[i].ToString() != "xx")
                            bytes++;


                    TSbox.Text = tstate.ToString();
                    Timebox.Text = (Mcycle * 2).ToString();
                    Bytebox.Text = bytes.ToString();
                    MCbox.Text = Mcycle.ToString();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "CF")                   //    RST 1
                {

                    tstate += 12;
                    Mcycle += 3;

                    bytes = 0;
                    for (int i = 0; i < 999; i++)
                        if (mem[i].ToString() != "xx")
                            bytes++;


                    TSbox.Text = tstate.ToString();
                    Timebox.Text = (Mcycle * 2).ToString();
                    Bytebox.Text = bytes.ToString();
                    MCbox.Text = Mcycle.ToString();
                    Updatemem();

                    break;
                }

                if (mem[PROG_CNT].ToString() == "D7")                   //    RST 2
                {
                    tstate += 12;
                    Mcycle += 3;

                    bytes = 0;
                    for (int i = 0; i < 999; i++)
                        if (mem[i].ToString() != "xx")
                            bytes++;


                    TSbox.Text = tstate.ToString();
                    Timebox.Text = (Mcycle * 2).ToString();
                    Bytebox.Text = bytes.ToString();
                    MCbox.Text = Mcycle.ToString();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "DF")                   //    RST 3
                {

                    tstate += 12;
                    Mcycle += 3;

                    bytes = 0;
                    for (int i = 0; i < 999; i++)
                        if (mem[i].ToString() != "xx")
                            bytes++;


                    TSbox.Text = tstate.ToString();
                    Timebox.Text = (Mcycle * 2).ToString();
                    Bytebox.Text = bytes.ToString();
                    MCbox.Text = Mcycle.ToString();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "E7")                   //    RST 4
                {

                    tstate += 12;
                    Mcycle += 3;

                    bytes = 0;
                    for (int i = 0; i < 999; i++)
                        if (mem[i].ToString() != "xx")
                            bytes++;


                    TSbox.Text = tstate.ToString();
                    Timebox.Text = (Mcycle * 2).ToString();
                    Bytebox.Text = bytes.ToString();
                    MCbox.Text = Mcycle.ToString();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "EF")                   //    RST 5
                {

                    tstate += 12;
                    Mcycle += 3;

                    bytes = 0;
                    for (int i = 0; i < 999; i++)
                        if (mem[i].ToString() != "xx")
                            bytes++;


                    TSbox.Text = tstate.ToString();
                    Timebox.Text = (Mcycle * 2).ToString();
                    Bytebox.Text = bytes.ToString();
                    MCbox.Text = Mcycle.ToString();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "F7")                   //    RST 6
                {

                    tstate += 12;
                    Mcycle += 3;

                    bytes = 0;
                    for (int i = 0; i < 999; i++)
                        if (mem[i].ToString() != "xx")
                            bytes++;


                    TSbox.Text = tstate.ToString();
                    Timebox.Text = (Mcycle * 2).ToString();
                    Bytebox.Text = bytes.ToString();
                    MCbox.Text = Mcycle.ToString();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "FF")                   //    RST 7
                {

                    tstate += 12;
                    Mcycle += 3;


                    // break;
                    //   ADDR.Text = "Hello FF";
                    bytes = 0;
                    for (int i = 0; i < 999; i++)
                        if (mem[i].ToString() != "xx")
                            bytes++;


                    TSbox.Text = tstate.ToString();
                    Timebox.Text = (Mcycle * 2).ToString();
                    Bytebox.Text = bytes.ToString();
                    MCbox.Text = Mcycle.ToString();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "76")                   //    HLT
                {

                    tstate += 5;
                    Mcycle += 2;

                    bytes = 0;
                    for (int i = 0; i < 999; i++)
                        if (mem[i].ToString() != "xx")
                            bytes++;


                    TSbox.Text = tstate.ToString();
                    Timebox.Text = (Mcycle * 2).ToString();
                    Bytebox.Text = bytes.ToString();
                    MCbox.Text = Mcycle.ToString();
                    break;
                  //  ADDR.Text = "Hello Tonu";
                  //  PROG_CNT++;
                }




                //             Stack Related


                if (mem[PROG_CNT].ToString() == "CD")                    //       CALL ppqq
                {


                    PROG_CNT += 3;
                    string s = dtohex(PROG_CNT);
                    int len = 4 - s.Length;
                    for (int i = 0; i < len; i++)
                        s = "0" + s;

                    push(s.Substring(s.Length - 2, 2));
                    push(s.Substring(s.Length - 4, 2));

                    PROG_CNT -= 2;
                    string jstr = mem[PROG_CNT].ToString();

                    jstr = mem[++PROG_CNT].ToString() + jstr;
                    PROG_CNT = hextod(jstr);

                    DT.Text = dtohex(PROG_CNT);
                    Mcycle += 5;
                    tstate += 18;
                    showregs();
                    break;

                }

                if (mem[PROG_CNT].ToString() == "C9")               //       RET      
                {
                    string s1 = pop();
                    string s2;

                    if (s1 != "xx")
                    {
                        s2 = pop();
                        if (s2 != "xx")
                        {
                            s2 = s1 + s2;

                            PROG_CNT = hextod(s2);
                        }
                    }
                    Mcycle += 3;
                    tstate += 10;
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "DC")                    //       CC ppqq
                {

                    if (fc == 1)
                    {
                        PROG_CNT += 3;
                        string s = dtohex(PROG_CNT);
                        int len = 4 - s.Length;
                        for (int i = 0; i < len; i++)
                            s = "0" + s;

                        push(s.Substring(s.Length - 2, 2));
                        push(s.Substring(s.Length - 4, 2));

                        PROG_CNT -= 2;
                        string jstr = mem[PROG_CNT].ToString();

                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);

                        DT.Text = dtohex(PROG_CNT);
                        Mcycle += 5;
                        tstate += 18;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 9;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "D4")                    //       CNC ppqq
                {

                    if (fc != 1)
                    {
                        PROG_CNT += 3;
                        string s = dtohex(PROG_CNT);
                        int len = 4 - s.Length;
                        for (int i = 0; i < len; i++)
                            s = "0" + s;

                        push(s.Substring(s.Length - 2, 2));
                        push(s.Substring(s.Length - 4, 2));

                        PROG_CNT -= 2;
                        string jstr = mem[PROG_CNT].ToString();

                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);

                        DT.Text = dtohex(PROG_CNT);
                        Mcycle += 5;
                        tstate += 18;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 9;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "CC")                    //       CZ ppqq
                {

                    if (fz == 1)
                    {
                        PROG_CNT += 3;
                        string s = dtohex(PROG_CNT);
                        int len = 4 - s.Length;
                        for (int i = 0; i < len; i++)
                            s = "0" + s;

                        push(s.Substring(s.Length - 2, 2));
                        push(s.Substring(s.Length - 4, 2));

                        PROG_CNT -= 2;
                        string jstr = mem[PROG_CNT].ToString();

                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);

                        DT.Text = dtohex(PROG_CNT);
                        Mcycle += 5;
                        tstate += 18;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 9;
                    }
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "C$")                    //       CNZ ppqq
                {

                    if (fz != 1)
                    {
                        PROG_CNT += 3;
                        string s = dtohex(PROG_CNT);
                        int len = 4 - s.Length;
                        for (int i = 0; i < len; i++)
                            s = "0" + s;

                        push(s.Substring(s.Length - 2, 2));
                        push(s.Substring(s.Length - 4, 2));

                        PROG_CNT -= 2;
                        string jstr = mem[PROG_CNT].ToString();

                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);

                        DT.Text = dtohex(PROG_CNT);
                        Mcycle += 5;
                        tstate += 18;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 9;
                    }
                    showregs();
                    break;
                }



                if (mem[PROG_CNT].ToString() == "FC")                    //       CM ppqq
                {
                    if (fs == 1)

                    {
                        PROG_CNT += 3;
                        string s = dtohex(PROG_CNT);
                        int len = 4 - s.Length;
                        for (int i = 0; i < len; i++)
                            s = "0" + s;

                        push(s.Substring(s.Length - 2, 2));
                        push(s.Substring(s.Length - 4, 2));

                        PROG_CNT -= 2;
                        string jstr = mem[PROG_CNT].ToString();

                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);

                        DT.Text = dtohex(PROG_CNT);
                        Mcycle += 5;
                        tstate += 18;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 9;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "F4")                    //       CP ppqq
                {
                    if (fs != 1)

                    {
                        PROG_CNT += 3;
                        string s = dtohex(PROG_CNT);
                        int len = 4 - s.Length;
                        for (int i = 0; i < len; i++)
                            s = "0" + s;

                        push(s.Substring(s.Length - 2, 2));
                        push(s.Substring(s.Length - 4, 2));

                        PROG_CNT -= 2;
                        string jstr = mem[PROG_CNT].ToString();

                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);

                        DT.Text = dtohex(PROG_CNT);
                        Mcycle += 5;
                        tstate += 18;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 9;
                    }
                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "EC")                    //       CPE ppqq
                {
                    if (fp == 1)

                    {
                        PROG_CNT += 3;
                        string s = dtohex(PROG_CNT);
                        int len = 4 - s.Length;
                        for (int i = 0; i < len; i++)
                            s = "0" + s;

                        push(s.Substring(s.Length - 2, 2));
                        push(s.Substring(s.Length - 4, 2));

                        PROG_CNT -= 2;
                        string jstr = mem[PROG_CNT].ToString();

                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);

                        DT.Text = dtohex(PROG_CNT);
                        Mcycle += 5;
                        tstate += 18;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 9;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "E4")                    //       CPO ppqq
                {
                    if (fp != 1)

                    {
                        PROG_CNT += 3;
                        string s = dtohex(PROG_CNT);
                        int len = 4 - s.Length;
                        for (int i = 0; i < len; i++)
                            s = "0" + s;

                        push(s.Substring(s.Length - 2, 2));
                        push(s.Substring(s.Length - 4, 2));

                        PROG_CNT -= 2;
                        string jstr = mem[PROG_CNT].ToString();

                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);

                        DT.Text = dtohex(PROG_CNT);
                        Mcycle += 5;
                        tstate += 18;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 9;
                    }
                    showregs();
                    break;
                }



                if (mem[PROG_CNT].ToString() == "F8")               //       RM      
                {

                    if (fs == 1)
                    {
                        string s1 = pop();
                        string s2;

                        if (s1 != "xx")
                        {
                            s2 = pop();
                            if (s2 != "xx")
                            {
                                s2 = s1 + s2;

                                PROG_CNT = hextod(s2);
                            }
                        }
                        Mcycle += 3;
                        tstate += 10;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;

                }


                if (mem[PROG_CNT].ToString() == "D0")               //       RNC     
                {

                    if (fc == 0)
                    {
                        string s1 = pop();
                        string s2;

                        if (s1 != "xx")
                        {
                            s2 = pop();
                            if (s2 != "xx")
                            {
                                s2 = s1 + s2;

                                PROG_CNT = hextod(s2);
                            }
                        }
                        Mcycle += 3;
                        tstate += 10;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "D8")               //       RC     
                {

                    if (fc != 0)
                    {
                        string s1 = pop();
                        string s2;

                        if (s1 != "xx")
                        {
                            s2 = pop();
                            if (s2 != "xx")
                            {
                                s2 = s1 + s2;

                                PROG_CNT = hextod(s2);
                            }
                        }
                        Mcycle += 3;
                        tstate += 10;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "C8")               //       RZ      
                {

                    if (fz != 0)
                    {
                        string s1 = pop();
                        string s2;

                        if (s1 != "xx")
                        {
                            s2 = pop();
                            if (s2 != "xx")
                            {
                                s2 = s1 + s2;

                                PROG_CNT = hextod(s2);
                            }
                        }
                        Mcycle += 3;
                        tstate += 10;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "C0")               //       RNZ      
                {

                    if (fz == 0)
                    {
                        string s1 = pop();
                        string s2;

                        if (s1 != "xx")
                        {
                            s2 = pop();
                            if (s2 != "xx")
                            {
                                s2 = s1 + s2;

                                PROG_CNT = hextod(s2);
                            }
                        }
                        Mcycle += 3;
                        tstate += 10;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "F0")               //       RP      
                {

                    if (fs == 0)
                    {
                        string s1 = pop();
                        string s2;

                        if (s1 != "xx")
                        {
                            s2 = pop();
                            if (s2 != "xx")
                            {
                                s2 = s1 + s2;

                                PROG_CNT = hextod(s2);
                            }
                        }
                        Mcycle += 3;
                        tstate += 10;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;

                }

                if (mem[PROG_CNT].ToString() == "E8")               //       RPE      
                {

                    if (fp == 1)
                    {
                        string s1 = pop();
                        string s2;

                        if (s1 != "xx")
                        {
                            s2 = pop();
                            if (s2 != "xx")
                            {
                                s2 = s1 + s2;

                                PROG_CNT = hextod(s2);
                            }
                        }
                        Mcycle += 3;
                        tstate += 10;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;

                }


                if (mem[PROG_CNT].ToString() == "EO")               //       RPO      
                {

                    if (fs == 0)
                    {
                        string s1 = pop();
                        string s2;

                        if (s1 != "xx")
                        {
                            s2 = pop();
                            if (s2 != "xx")
                            {
                                s2 = s1 + s2;

                                PROG_CNT = hextod(s2);
                            }
                        }
                        Mcycle += 3;
                        tstate += 10;
                    }

                    else
                    {
                        PROG_CNT += 2;
                        Mcycle += 2;
                        tstate += 7;
                    }
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "E9")                //      PCHL
                {

                    PROG_CNT = hextod(dtohex(rh) + dtohex(rl));
                   

                    string Tex = dtohex(PROG_CNT);
                    int lim = Tex.Length;
                    lim = 4 - lim;

                    for (int i = 0; i < lim; i++)
                        Tex = "0" + Tex;
                    Program_Counter.Text = Tex;


                    Mcycle += 1;
                    tstate += 6;
                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "C1")               //       POP B      
                {

                    rc = hextod(mem[STK_PNT++].ToString());
                    rd = hextod(mem[STK_PNT++].ToString());

                    PROG_CNT++;
                    Mcycle += 3;
                    tstate += 10;

                    showregs();
                    break;

                }

                if (mem[PROG_CNT].ToString() == "D1")               //       POP D     
                {

                    re = hextod(mem[STK_PNT++].ToString());
                    rd = hextod(mem[STK_PNT++].ToString());

                    showregs();

                    PROG_CNT++;
                    Mcycle += 3;
                    tstate += 10;
                    showregs();

                    break;

                }

                if (mem[PROG_CNT].ToString() == "E1")               //       POP H     
                {

                    rl = hextod(mem[STK_PNT++].ToString());
                    rh = hextod(mem[STK_PNT++].ToString());


                    PROG_CNT++;
                    Mcycle += 3;
                    tstate += 10;

                    showregs();
                    break;

                }

                if (mem[PROG_CNT].ToString() == "F1")               //       POP PSW    
                {

                    string s = mem[STK_PNT++].ToString();

                    ra = hextod(mem[STK_PNT++].ToString());

                    string bin = Convert.ToString(hextod(s), 2);

                    int lim = bin.Length;
                    lim = 8 - lim;

                    for (int i = 0; i < lim; i++)
                        bin = "0" + bin;


                    fs = Convert.ToInt32(bin.Substring(0, 1));
                    fz = Convert.ToInt32(bin.Substring(1, 1));

                    fac = Convert.ToInt32(bin.Substring(3, 1));

                    fp = Convert.ToInt32(bin.Substring(5, 1));

                    fc = Convert.ToInt32(bin.Substring(7, 1));

                    PROG_CNT++;

                    Mcycle += 3;


                    tstate += 12;

                    showregs();

                    break;

                }


                if (mem[PROG_CNT].ToString() == "C5")               //       PUSH B     
                {

                    string s1 = dtohex(rb);
                    string s2 = dtohex(rc);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;


                    if (STK_PNT != 0)
                    {
                        mem[STK_PNT] = s1;
                        mem[--STK_PNT] = s2;
                    }
                    else
                        GO_tb.Text = "Stack Is Not Initialized !!!";
                  
                    PROG_CNT++;
                    Mcycle += 3;
                    tstate += 12;

                    showregs();
                    break;
                }


                if (mem[PROG_CNT].ToString() == "D5")               //       PUSH D     
                {

                    string s1 = dtohex(rd);
                    string s2 = dtohex(re);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    if (STK_PNT != 0)
                    {
                        mem[STK_PNT] = s1;
                        mem[--STK_PNT] = s2;
                    }
                    else
                        GO_tb.Text = "Stack Is Not Initialized !!!";

                    PROG_CNT++;
                    Mcycle += 3;
                    tstate += 12;

                    showregs();
                    break;

                }

                if (mem[PROG_CNT].ToString() == "E5")               //       PUSH H     
                {

                    string s1 = dtohex(rh);
                    string s2 = dtohex(rl);

                    if (s1.Length == 1)
                        s1 = "0" + s1;
                    if (s2.Length == 1)
                        s2 = "0" + s2;

                    if (STK_PNT != 0)
                    {
                        mem[STK_PNT] = s1;
                        mem[--STK_PNT] = s2;
                    }
                    else
                        GO_tb.Text = "Stack Is Not Initialized !!!";


                    PROG_CNT++;
                    Mcycle += 3;
                    tstate += 12;

                    showregs();
                    break;
                }

                if (mem[PROG_CNT].ToString() == "F5")               //       PUSH PSW     
                {

                    if (STK_PNT != 0)
                    {
                        string o = dtohex(ra);

                        if (o.Length == 1)
                            o = "0" + o;

                        mem[STK_PNT--] = o;

                        string lv = fs.ToString() + fz.ToString() + "0" + fac.ToString() + "0" + fp.ToString() + "0" + fc.ToString();

                        string oo = dtohex(Convert.ToInt32(lv, 2));

                        if (oo.Length == 1)
                            oo = "0" + oo;

                        mem[STK_PNT] = oo;
                        PROG_CNT++;
                        showregs();

                        Mcycle += 3;
                        tstate += 10;
                    }

                    else
                        GO_tb.Text = "Stack Is Not Initialized !!!";
                    break;
                }

                if (mem[PROG_CNT].ToString() == "xx")
                {
                    GO_tb.Text = "Invalid Data or Code Encountered !!!";
                    break;
                }





                bytes = 0;
                for (int i = 0; i < 999; i++)
                    if (mem[i].ToString() != "xx")
                        bytes++;


                TSbox.Text = tstate.ToString();
                Timebox.Text = (Mcycle * 2).ToString();
                Bytebox.Text = bytes.ToString();
                MCbox.Text = Mcycle.ToString();

            }
            //  ADDR.Text = "END";
        }


        private void RUN_Click(object sender, EventArgs e)
        {

            // string test = mem[PROG_CNT].ToString();

            if (PROG_CNT < 0 || PROG_CNT > 999)
            {
                GO_tb.Text = "Invalid Base Address";

            }

            else
            {
                if (mem[PROG_CNT] == null)
                {
                    GO_tb.Text = "Please Start with address 0";
                    return;
                }
                while (mem[PROG_CNT].ToString() != "xx")
                {
                    // MVI Instruction //

                    if (mem[PROG_CNT].ToString() == "3E")                 //  MVI A,DATA
                    {

                        PROG_CNT = PROG_CNT + 1;
                        string t = mem[PROG_CNT].ToString();
                        ra = hextod(t);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;

                    }

                    if (mem[PROG_CNT].ToString() == "06")                  //  MVI B,DATA
                    {
                        rb = hextod(mem[++PROG_CNT].ToString());
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "0E")                   //  MVI C,DATA
                    {
                        rc = hextod(mem[++PROG_CNT].ToString());
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "16")                   //  MVI D,DATA
                    {
                        rd = hextod(mem[++PROG_CNT].ToString());
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "1E")                   //   MVI E,DATA
                    {
                        re = hextod(mem[++PROG_CNT].ToString());
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "26")                    //   MVI H,DATA
                    {
                        rh = hextod(mem[++PROG_CNT].ToString());
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "2E")                    //   MVI L,DATA
                    {
                        rl = hextod(mem[++PROG_CNT].ToString());
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "36")                    //    MVI M[HL],DATA
                    {
                        mem[hextod(dtohex(rh) + dtohex(rl))] = mem[++PROG_CNT];
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 3;
                        tstate += 13;
                    }


                    // LXI Instruction

                    if (mem[PROG_CNT].ToString() == "01")                 //  LXI  BC , MEM
                    {
                        string t1 = mem[++PROG_CNT].ToString();
                        rc = hextod(t1);
                        string t2 = mem[++PROG_CNT].ToString();
                        rb = hextod(t2);

                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 3;
                        tstate += 10;
                    }

                    if (mem[PROG_CNT].ToString() == "11")                 //  LXI  DE , MEM
                    {
                        string t1 = mem[++PROG_CNT].ToString();
                        re = hextod(t1);
                        string t2 = mem[++PROG_CNT].ToString();
                        rd = hextod(t2);

                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 3;
                        tstate += 10;

                    }

                    if (mem[PROG_CNT].ToString() == "21")                 //  LXI  HL , MEM
                    {
                        string t1 = mem[++PROG_CNT].ToString();
                        rl = hextod(t1);
                        string t2 = mem[++PROG_CNT].ToString();
                        rh = hextod(t2);

                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 3;
                        tstate += 10;
                    }

                    if (mem[PROG_CNT].ToString() == "31")                 //  LXI  SP , MEM
                    {

                        string t = mem[++PROG_CNT].ToString();
                        t = mem[++PROG_CNT].ToString() + t;
                        STK_PNT = (hextod(t));

                        PROG_CNT++;
                        DT.Text = dtohex(STK_PNT);
                        Mcycle += 3;
                        tstate += 10;
                    }


                    //  MOV Instruction  //                        
                    if (mem[PROG_CNT].ToString() == "7F")                   //   MOV A,A
                    {
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "78")                   //   MOV A,B
                    {
                        ra = rb;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "79")                    //   MOV A,C
                    {
                        ra = rc;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "7A")                     //   MOV A,D
                    {
                        ra = rd;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "7B")                     //    MOV A,E
                    {
                        ra = re;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "7C")                      //    MOV A,H
                    {
                        ra = rh;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "7D")                      //    MOV A,L
                    {
                        ra = rl;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "7E")                      //     MOV A,M[HL]
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        ra = hextod(mem[hextod(s1 + s2)].ToString());

                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;

                    }

                    if (mem[PROG_CNT].ToString() == "47")                      //    MOV B,A
                    {
                        rb = ra;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "40")                      //    MOV B,B
                    {
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "41")                      //    MOV B,C
                    {
                        rb = rc;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "42")                      //    MOV B,D
                    {
                        rb = rd;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "43")                      //    MOV B,E
                    {
                        rb = re;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "44")                      //    MOV B,H
                    {
                        rb = rh;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "45")                      //    MOV B,L
                    {
                        rb = rl;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "46")                      //    MOV B,M[HL]
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        rb = hextod(mem[hextod(s1 + s2)].ToString());

                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;

                    }

                    if (mem[PROG_CNT].ToString() == "4F")                      //    MOV C,A
                    {
                        rc = ra;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "48")                      //    MOV C,B
                    {
                        rc = rb;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "49")                      //    MOV C,C
                    {
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "4A")                      //    MOV C,D
                    {
                        rc = rd;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "4B")                      //    MOV C,E
                    {
                        rc = re;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "4C")                      //    MOV C,H
                    {
                        rc = rh;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "4D")                      //    MOV C,L
                    {
                        rc = rl;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "4E")                      //    MOV C,M[HL]
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        rc = hextod(mem[hextod(s1 + s2)].ToString());

                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;

                    }

                    if (mem[PROG_CNT].ToString() == "57")                      //    MOV D,A
                    {
                        rd = ra;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "50")                      //    MOV D,B
                    {
                        rd = rb;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "51")                      //    MOV D,C
                    {
                        rd = rc;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "52")                      //    MOV D,D
                    {
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "53")                      //    MOV D,E
                    {
                        rd = re;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "54")                      //    MOV D,H
                    {
                        rd = rh;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "55")                      //    MOV D,L
                    {
                        rd = rl;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "56")                      //    MOV D,M[HL]
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        rd = hextod(mem[hextod(s1 + s2)].ToString());

                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;

                    }

                    if (mem[PROG_CNT].ToString() == "5F")                      //    MOV E,A
                    {
                        re = ra;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "58")                      //    MOV E,B
                    {
                        re = rb;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "59")                      //    MOV E,C
                    {
                        re = rc;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "5A")                      //    MOV E,D
                    {
                        re = rd;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "5B")                      //    MOV E,E
                    {
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "5C")                      //    MOV E,H
                    {
                        re = rh;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "5D")                      //    MOV E,L
                    {
                        re = rl;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "5E")                      //    MOV E,M[HL]
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        re = hextod(mem[hextod(s1 + s2)].ToString());

                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;

                    }

                    if (mem[PROG_CNT].ToString() == "67")                      //    MOV H,A
                    {
                        rh = ra;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "60")                      //    MOV H,B
                    {
                        rh = rb;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "61")                      //    MOV H,C
                    {
                        rh = rc;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "62")                      //    MOV H,D
                    {
                        rh = rd;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "63")                      //    MOV H,E
                    {
                        rh = re;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "64")                      //    MOV H,H
                    {
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "65")                      //    MOV H,L
                    {
                        rh = rl;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "5E")                      //    MOV H,M[HL]
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        rh = hextod(mem[hextod(s1 + s2)].ToString());

                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;

                    }

                    if (mem[PROG_CNT].ToString() == "6F")                      //    MOV L,A
                    {
                        rl = ra;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "68")                      //    MOV L,B
                    {
                        rl = rb;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "69")                      //    MOV L,C
                    {
                        rl = rc;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "6A")                      //    MOV L,D
                    {
                        rl = rd;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "6B")                      //    MOV L,E
                    {
                        rl = re;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "6C")                      //    MOV L,H
                    {
                        rl = rh;
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "6D")                      //    MOV L,L
                    {
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "6E")                      //    MOV L,M[HL]
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        rl = hextod(mem[hextod(s1 + s2)].ToString());

                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;

                    }

                    if (mem[PROG_CNT].ToString() == "77")                       //   MOV M[HL],A
                    {

                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        mem[hextod(s1 + s2)] = dtohex(ra);
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;

                    }

                    if (mem[PROG_CNT].ToString() == "70")                       //   MOV M[HL],B
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        mem[hextod(s1 + s2)] = dtohex(rb);
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "71")                       //   MOV M[HL],C
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        mem[hextod(s1 + s2)] = dtohex(rc);
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "72")                       //   MOV M[HL],D
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        mem[hextod(s1 + s2)] = dtohex(rd);
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "73")                       //   MOV M[HL],E
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        mem[hextod(s1 + s2)] = dtohex(re);
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "74")                       //   MOV M[HL],H
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        mem[hextod(s1 + s2)] = dtohex(rh);
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "75")                       //   MOV M[HL],L
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        mem[hextod(s1 + s2)] = dtohex(rl);
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }


                    //    INR Instructions   //


                    if (mem[PROG_CNT].ToString() == "3C")           // INR A
                    {
                        ra++;
                        PROG_CNT++;
                        updateflags(1, 1, 0, 1, 1);
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "04")           // INR B
                    {
                        rb++;
                        PROG_CNT++;
                        updateflags(2, 2, 0, 2, 2);
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "0C")           // INR C
                    {
                        rc++;
                        PROG_CNT++;
                        updateflags(3, 3, 0, 3, 3);
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "14")           // INR D
                    {
                        rd++;
                        PROG_CNT++;
                        updateflags(4, 4, 0, 4, 4);
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "1C")           // INR E
                    {
                        re++;
                        PROG_CNT++;
                        updateflags(5, 5, 0, 5, 5);
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "24")           // INR H
                    {
                        rh++;
                        PROG_CNT++;
                        updateflags(6, 6, 0, 6, 6);
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "2C")           // INR L
                    {
                        rl++;
                        PROG_CNT++;
                        updateflags(7, 7, 0, 7, 7);
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "34")           // INR M[HL]
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        int t = hextod(mem[hextod(s1 + s2)].ToString());

                        mem[hextod(s1 + s2)] = dtohex(++t);
                        updateflags(8, 8, 0, 8, 8);
                        showregs();
                        PROG_CNT++;
                        Mcycle += 3;
                        tstate += 10;
                    }

                    if (mem[PROG_CNT].ToString() == "03")           // INX BC
                    {

                        string s1 = dtohex(rb);
                        string s2 = dtohex(rc);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        int val = hextod(s1 + s2) + 1;

                        string vals = dtohex(val);
                        int len = vals.Length;

                        if (len == 3)
                            vals = "0" + vals;
                        else if (len == 2)
                            vals = "00" + vals;
                        else if (len == 1)
                            vals = "000" + vals;

                        int len2 = vals.Length;

                        rc = hextod(vals.Substring(2, 2));
                        rb = hextod(vals.Substring(0, 2));
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 6;
                    }

                    if (mem[PROG_CNT].ToString() == "13")           // INX DE
                    {
                        string s1 = dtohex(rd);
                        string s2 = dtohex(re);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        int val = hextod(s1 + s2) + 1;

                        string vals = dtohex(val);
                        int len = vals.Length;

                        if (len == 3)
                            vals = "0" + vals;
                        else if (len == 2)
                            vals = "00" + vals;
                        else if (len == 1)
                            vals = "000" + vals;

                        int len2 = vals.Length;

                        re = hextod(vals.Substring(2, 2));
                        rd = hextod(vals.Substring(0, 2));
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 6;

                    }

                    if (mem[PROG_CNT].ToString() == "23")           // INX HL
                    {

                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        int val = hextod(s1 + s2) + 1;

                        string vals = dtohex(val);
                        int len = vals.Length;

                        if (len == 3)
                            vals = "0" + vals;
                        else if (len == 2)
                            vals = "00" + vals;
                        else if (len == 1)
                            vals = "000" + vals;

                        int len2 = vals.Length;

                        rl = hextod(vals.Substring(2, 2));
                        rh = hextod(vals.Substring(0, 2));
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 6;
                    }

                    if (mem[PROG_CNT].ToString() == "33")           // INX SP
                    {
                        STK_PNT++;
                        PROG_CNT++;
                        Mcycle += 1;

                        tstate += 6;
                    }



                    // DCR Instructions//
                    if (mem[PROG_CNT].ToString() == "3D")           // DCR A
                    {
                        ra--;
                        updateflags(1, 1, 0, 1, 1);
                        PROG_CNT++;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "05")           // DCR B
                    {
                        rb--;
                        updateflags(2, 2, 0, 2, 2);
                        PROG_CNT++;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "0D")           // DCR C
                    {
                        rc--;
                        updateflags(3, 3, 0, 3, 3);
                        PROG_CNT++;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "15")           // DCR D
                    {
                        rd--;
                        updateflags(4, 4, 0, 4, 4);
                        PROG_CNT++;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "1D")           // DCR E
                    {
                        re--;
                        updateflags(5, 5, 0, 5, 5);
                        PROG_CNT++;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "25")           // DCR H
                    {
                        rh--;
                        updateflags(6, 6, 0, 6, 6);
                        PROG_CNT++;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "2D")           // DCR L
                    {
                        rl--;
                        updateflags(7, 7, 0, 7, 7);
                        PROG_CNT++;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "35")           // DCR M[HL]
                    {
                        string s1 = dtohex(rb);
                        string s2 = dtohex(rc);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        int val = hextod(s1 + s2) - 1;

                        string vals = dtohex(val);
                        int len = vals.Length;

                        if (len == 3)
                            vals = "0" + vals;
                        else if (len == 2)
                            vals = "00" + vals;
                        else if (len == 1)
                            vals = "000" + vals;

                        int len2 = vals.Length;

                        rc = hextod(vals.Substring(2, 2));
                        rb = hextod(vals.Substring(0, 2));
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 6;
                    }

                    if (mem[PROG_CNT].ToString() == "0B")           // DCX BC
                    {


                        string s1 = dtohex(rb);
                        string s2 = dtohex(rc);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        int val = hextod(s1 + s2) - 1;

                        string vals = dtohex(val);
                        int len = vals.Length;

                        if (len == 3)
                            vals = "0" + vals;
                        else if (len == 2)
                            vals = "00" + vals;
                        else if (len == 1)
                            vals = "000" + vals;

                        int len2 = vals.Length;

                        rc = hextod(vals.Substring(2, 2));
                        rb = hextod(vals.Substring(0, 2));
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 6;
                    }

                    if (mem[PROG_CNT].ToString() == "1B")           // DCX DE
                    {
                        string s1 = dtohex(rd);
                        string s2 = dtohex(re);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        int val = hextod(s1 + s2) - 1;

                        string vals = dtohex(val);
                        int len = vals.Length;

                        if (len == 3)
                            vals = "0" + vals;
                        else if (len == 2)
                            vals = "00" + vals;
                        else if (len == 1)
                            vals = "000" + vals;

                        int len2 = vals.Length;

                        re = hextod(vals.Substring(2, 2));
                        rd = hextod(vals.Substring(0, 2));
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 6;
                    }

                    if (mem[PROG_CNT].ToString() == "2B")           // DCX HL
                    {

                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        int val = hextod(s1 + s2) - 1;

                        string vals = dtohex(val);
                        int len = vals.Length;

                        if (len == 3)
                            vals = "0" + vals;
                        else if (len == 2)
                            vals = "00" + vals;
                        else if (len == 1)
                            vals = "000" + vals;

                        int len2 = vals.Length;

                        rl = hextod(vals.Substring(2, 2));
                        rh = hextod(vals.Substring(0, 2));
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 6;
                    }

                    if (mem[PROG_CNT].ToString() == "3B")           // DCX SP
                    {

                        STK_PNT--;
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 6;

                    }



                    //LDA Instruction//
                    if (mem[PROG_CNT].ToString() == "3A")           // LDA
                    {
                        string h, l, t;
                        l = mem[++PROG_CNT].ToString();
                        h = mem[++PROG_CNT].ToString();
                        t = h + l;

                        int va = hextod(t);


                        if (hextod(t) > 999 || hextod(t) < 0)
                        {
                            GO_tb.Text = "Wrong Address Given !!!";
                            break;
                        }

                        ra = hextod(mem[va].ToString());
                        showregs();
                        PROG_CNT++;
                        Mcycle += 3;
                        tstate += 10;
                    }



                    //  ADI instruction //                             
                    if (mem[PROG_CNT].ToString() == "C6")                  //  ADI DATA
                    {
                        ra += hextod(mem[++PROG_CNT].ToString());
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "CE")                  //  ACI DATA
                    {
                        ra += hextod(mem[++PROG_CNT].ToString());
                        ra += fc;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "8F")                  //  ADC A
                    {
                        ra = ra + ra + fc;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "88")                  //  ADC B
                    {
                        ra = ra + rb + fc;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "89")                  //  ADC C
                    {
                        ra = ra + rc + fc;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "8A")                  //  ADC D
                    {
                        ra = ra + rd + fc;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "8B")                  //  ADC E
                    {
                        ra = ra + re + fc;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "8C")                  //  ADC H
                    {
                        ra = ra + rh + fc;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "8D")                  //  ADC L
                    {
                        ra = ra + rl + fc;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "8E")                  //  ADC M
                    {

                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        int i69 = hextod(s1 + s2);
                        string s108 = mem[i69].ToString();
                        ra = ra + fc + hextod(s108);
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT++;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;
                    }


                    if (mem[PROG_CNT].ToString() == "87")                  //  ADD A
                    {
                        ra = ra + ra;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }



                    if (mem[PROG_CNT].ToString() == "80")                  //  ADD B
                    {

                        ra = ra + rb;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "81")                  //  ADD C
                    {
                        ra = ra + rc;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "82")                  //  ADD D
                    {
                        ra = ra + rd;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "83")                  //  ADD E
                    {
                        ra = ra + re;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "84")                  //  ADD H
                    {
                        ra = ra + rh;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT++;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "85")                  //  ADD L
                    {
                        ra = ra + rl;
                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT++;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "86")                  //  ADD M
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        int i69 = hextod(s1 + s2);
                        string s108 = mem[i69].ToString();

                        ra = ra + hextod(s108);

                        updateflags(1, 1, 1, 1, 1);
                        PROG_CNT++;
                        showregs();
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "A7")                    //  ANA A
                    {
                        ra = ra & ra;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 1;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "A0")                    //  ANA B
                    {
                        ra = ra & rb;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 1;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "A1")                    //  ANA C
                    {
                        ra = ra & rc;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 1;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "A2")                    //  ANA D
                    {
                        ra = ra & rd;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 1;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "A3")                    //  ANA E
                    {
                        ra = ra & re;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 1;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "A4")                    //  ANA H
                    {
                        ra = ra & rh;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 1;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "A5")                    //  ANA L
                    {
                        ra = ra & rl;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 1;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "A6")                    //  ANA M[HL]
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        ra = ra & hextod(mem[hextod(s1 + s2)].ToString());
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 1;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "E6")                    //  ANI DATA
                    {
                        ra = ra & hextod(mem[++PROG_CNT].ToString());
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 1;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "00")                      //   NOP
                    {
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "B7")                       //  ORA A
                    {
                        ra = ra | ra;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }
                    if (mem[PROG_CNT].ToString() == "B0")                       //  ORA B
                    {
                        ra = ra | rb;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }
                    if (mem[PROG_CNT].ToString() == "B1")                       //  ORA C
                    {
                        ra = ra | rc;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }
                    if (mem[PROG_CNT].ToString() == "B2")                       //  ORA D
                    {
                        ra = ra | rd;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }
                    if (mem[PROG_CNT].ToString() == "B3")                       //  ORA E
                    {
                        ra = ra | re;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }
                    if (mem[PROG_CNT].ToString() == "B4")                       //  ORA H
                    {
                        ra = ra | rh;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "B5")                       //  ORA L
                    {
                        ra = ra | rl;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "B6")                       //  ORA M[HL]
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        ra = ra | hextod(mem[hextod(s1 + s2)].ToString());
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "F6")                       //  ORA DATA
                    {
                        ra = ra | hextod(mem[++PROG_CNT].ToString());
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "AF")                        //   XRA A
                    {
                        ra = ra ^ ra;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }
                    if (mem[PROG_CNT].ToString() == "A8")                        //   XRA B
                    {
                        ra = ra ^ rb;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "A9")                        //   XRA C
                    {
                        ra = ra ^ rc;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "AA")                        //   XRA D
                    {
                        ra = ra ^ rd;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "AB")                        //   XRA E
                    {
                        ra = ra ^ re;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "AC")                        //   XRA H
                    {
                        ra = ra ^ rh;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "AD")                        //   XRA L
                    {
                        ra = ra ^ rl;
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "AE")                        //   XRA M[HL]
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        ra = ra ^ hextod(mem[hextod(s1 + s2)].ToString());
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "EE")                        //   XRI DATA
                    {
                        ra = ra ^ hextod(mem[++PROG_CNT].ToString());
                        updateflags(1, 1, 1, 1, 1);
                        fc = 0;
                        fac = 0;
                        showregs();
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                    }


                    if (mem[PROG_CNT].ToString() == "9F")                        //   SBB A;
                    {
                        ra = ra - ra - fc;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "98")                        //   SBB B;
                    {
                        ra = ra - rb - fc;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "99")                        //   SBB C;
                    {
                        ra = ra - rc - fc;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "9A")                        //   SBB D;
                    {
                        ra = ra - rd - fc;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "9B")                        //   SBB E;
                    {
                        ra = ra - re - fc;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "9C")                        //   SBB H;
                    {
                        ra = ra - rh - fc;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "9D")                        //   SBB L;
                    {
                        ra = ra - rl - fc;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "9E")                        //   SBB M[HL];
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        ra = ra - hextod(mem[hextod(s1 + s2)].ToString()) - fc;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "DE")                        //   SBB DATA;
                    {
                        ra = ra - hextod(mem[++PROG_CNT].ToString()) - fc;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 2;
                        tstate += 7;
                    }


                    if (mem[PROG_CNT].ToString() == "97")                        //   SUB A;
                    {
                        ra = ra - ra;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "90")                        //   SUB B;
                    {
                        ra = ra - rb;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "91")                        //   SUB C;
                    {
                        ra = ra - rc;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "92")                        //   SUB D;
                    {
                        ra = ra - rd;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "93")                        //   SUB E;
                    {
                        ra = ra - re;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "94")                        //   SUB H;
                    {
                        ra = ra - rh;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "95")                        //   SUB L;
                    {
                        ra = ra - rl;
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "96")                        //   SUB M[HL];
                    {
                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        ra = ra - hextod(mem[hextod(s1 + s2)].ToString());
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "D6")                        //   SUI DATA;
                    {

                        ra = ra - hextod(mem[++PROG_CNT].ToString());
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 2;
                        tstate += 7;
                    }


                    if (mem[PROG_CNT].ToString() == "27")                        //   DAA
                    {
                        RACC.Text = hextod(dtohex(ra)).ToString();
                        updateflags(1, 1, 1, 1, 1);
                        showregs();
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 10;
                    }

                    if (mem[PROG_CNT].ToString() == "09")                        //   DAD B
                    {
                        rl = rl + rc;
                        rh = rh + rb;
                        updateflags(0, 0, 7, 0, 0);

                        if (fc == 1)
                            rh += 1;
                        showregs();

                        showregs();
                        PROG_CNT++;
                        Mcycle += 3;
                        tstate += 10;

                    }

                    if (mem[PROG_CNT].ToString() == "19")                        //   DAD D
                    {
                        rl = rl + re;
                        rh = rh + rd;
                        updateflags(0, 0, 7, 0, 0);
                        if (fc == 1)
                            rh += 1;
                        showregs();
                        updateflags(0, 0, 7, 0, 0);
                        showregs();
                        PROG_CNT++;
                        Mcycle += 3;
                        tstate += 10;
                    }

                    if (mem[PROG_CNT].ToString() == "29")                        //   DAD H
                    {
                        rl = rl + rl;
                        rh = rh + rh;
                        updateflags(0, 0, 7, 0, 0);
                        if (fc == 1)
                            rh += 1;
                        showregs();
                        updateflags(0, 0, 7, 0, 0);
                        showregs();
                        PROG_CNT++;
                        Mcycle += 3;
                        tstate += 10;
                    }

                    if (mem[PROG_CNT].ToString() == "39")                        //   DAD SP
                    {
                        rl = rl + rl;
                        rh = rh + rh;
                        updateflags(0, 0, 7, 0, 0);
                        if (fc == 1)
                            rh += 1;
                        showregs();
                        updateflags(0, 0, 7, 0, 0);
                        showregs();
                        Mcycle += 3;
                        PROG_CNT++;
                    }


                    if (mem[PROG_CNT].ToString() == "0A")                        //   LDAX M[BC];
                    {
                        ra = hextod(mem[hextod(dtohex(rb) + dtohex(rc))].ToString());
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 2;
                        tstate += 7;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "1A")                        //   LDAX M[DE];
                    {
                        ra = hextod(mem[hextod(dtohex(rd) + dtohex(re))].ToString());
                        PROG_CNT = PROG_CNT + 1;
                        Mcycle += 2;
                        tstate += 7;
                    }

                    if (mem[PROG_CNT].ToString() == "2A")                        //   LHLD ppqq;
                    {
                        string p = mem[++PROG_CNT].ToString();
                        p = mem[++PROG_CNT].ToString() + p;

                        rl = hextod(mem[hextod(p)].ToString());
                        rh = hextod(mem[hextod(p) + 1].ToString());
                        PROG_CNT = PROG_CNT + 1;
                        showregs();
                        Mcycle += 5;
                        tstate += 16;
                    }

                    if (mem[PROG_CNT].ToString() == "22")                         //   SHLD ppqq
                    {
                        string st = mem[++PROG_CNT].ToString();
                        st = mem[++PROG_CNT].ToString() + st;

                        mem[hextod(st)] = dtohex(rl);
                        mem[hextod(st) + 1] = dtohex(rh);
                        PROG_CNT++;
                        Mcycle += 5;
                        tstate += 16;
                    }

                    if (mem[PROG_CNT].ToString() == "F9")                         //     SPHL
                    {
                        STK_PNT = hextod(dtohex(rh) + dtohex(rl));
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 6;
                    }

                    if (mem[PROG_CNT].ToString() == "32")                         //     STA ppqq
                    {
                        string st = mem[++PROG_CNT].ToString();
                        st = mem[++PROG_CNT].ToString() + st;

                        string st2 = dtohex(ra);

                        if (hextod(st) > 999 || hextod(st) < 0)
                        {
                            GO_tb.Text = "Wrong Address Given !!!";
                            break;
                        }

                        if (st2.Length == 1)
                            st2 = "0" + st2;

                        mem[hextod(st)] = st2;
                        PROG_CNT++;
                        Mcycle += 4;
                        tstate += 13;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "02")                         //     STAX B
                    {
                        mem[hextod(dtohex(rb) + dtohex(rc))] = dtohex(ra);
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "12")                         //     STAX D
                    {
                        mem[hextod(dtohex(rd) + dtohex(re))] = dtohex(ra);
                        PROG_CNT++;
                        Mcycle += 2;
                        tstate += 7;
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "EB")                         //     XCNG
                    {
                        int w, z;
                        w = rh;
                        z = rl;

                        rh = rd;
                        rl = re;

                        rd = w;
                        re = z;

                        PROG_CNT++;
                        showregs();
                        Mcycle += 1;
                        tstate += 4;
                    }

                    if (mem[PROG_CNT].ToString() == "E3")                         //      XTHL
                    {
                        int a, b;

                        a = hextod(mem[STK_PNT].ToString());
                        b = hextod(mem[STK_PNT + 1].ToString());

                        mem[STK_PNT] = rl;
                        mem[STK_PNT + 1] = rh;

                        rl = a;
                        rh = b;


                        PROG_CNT++;

                        showregs();
                        Mcycle += 5;
                        tstate += 16;

                    }



                    if (mem[PROG_CNT].ToString() == "37")                        //     STC
                    {
                        fc = 1;
                       
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "2F")                       //      CMA A
                    {

                        // ra = 10;

                        //  ra = ~ra;
                        string tem = Convert.ToString(ra, 2);
                        string res = null;

                        int lim = tem.Length;
                        lim = 8 - lim;

                        for (int i = 0; i < lim; i++)
                            tem = "0" + tem;

                        for (int i = 0; i <= 7; i++)
                        {

                            if (tem.Substring(i, 1) == "1")
                                res = res + "0";
                            else
                                res = res + "1";

                        }

                        ra = Convert.ToInt32(res, 2);

                      
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "3F")                       //      CMC 
                    {
                        if (fc == 1)
                            fc = 0;
                        else
                            fc = 1;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "BF")                        //     CMP A
                    {
                        int tra = ra;
                        ra = ra - ra;

                        updateflags(1, 1, 1, 1, 1);
                        ra = tra;
                      
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "B8")                        //     CMP B
                    {
                        int tra = ra;
                        ra = ra - rb;

                        updateflags(1, 1, 1, 1, 1);
                        ra = tra;
                       
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "B9")                        //     CMP C
                    {
                        int tra = ra;
                        ra = ra - rc;

                        updateflags(1, 1, 1, 1, 1);
                        ra = tra;
                      
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "BA")                        //     CMP D
                    {
                        int tra = ra;
                        ra = ra - rd;

                        updateflags(1, 1, 1, 1, 1);
                        ra = tra;
                       
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "BB")                        //     CMP E
                    {
                        int tra = ra;
                        ra = ra - re;

                        updateflags(1, 1, 1, 1, 1);
                        ra = tra;
                      
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "BC")                        //     CMP H
                    {
                        int tra = ra;
                        ra = ra - rh;

                        updateflags(1, 1, 1, 1, 1);
                        ra = tra;
                       
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "BD")                        //     CMP L
                    {
                        int tra = ra;
                        ra = ra - rl;

                        updateflags(1, 1, 1, 1, 1);
                        ra = tra;
                       
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "BE")                        //     CMP M
                    {

                        int tra = ra;
                        ra = ra - hextod(mem[hextod(dtohex(rh) + dtohex(rl))].ToString());

                        updateflags(1, 1, 1, 1, 1);
                        ra = tra;
                       
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 7;
                        showregs();
                    }


                    //                         Brunch Instructions                           //


                    if (mem[PROG_CNT].ToString() == "DA")           // JC ppqq
                    {

                        if (fc == 1)
                        {
                            string jstr = mem[++PROG_CNT].ToString();
                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);
                            Mcycle += 3;
                            tstate += 10;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "FA")           // JM ppqq
                    {

                        if (fs == 1)
                        {
                            string jstr = mem[++PROG_CNT].ToString();
                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);
                            Mcycle += 3;
                            tstate += 10;
                        }
                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "C3")           // JMP ppqq
                    {

                        string jstr = mem[++PROG_CNT].ToString();
                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);
                        Mcycle += 3;
                        tstate += 10;
                        showregs();
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "D2")           // JNC ppqq
                    {

                        if (fc == 0)
                        {
                            string jstr = mem[++PROG_CNT].ToString();
                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);
                            Mcycle += 3;
                            tstate += 10;
                        }
                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "C2")           // JNZ ppqq
                    {

                        if (fz == 0)
                        {
                            string jstr = mem[++PROG_CNT].ToString();
                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);
                            Mcycle += 3;
                            tstate += 10;
                        }
                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "F2")           // JP ppqq
                    {

                        if (fs == 0)
                        {
                            string jstr = mem[++PROG_CNT].ToString();
                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);
                            Mcycle += 3;
                            tstate += 10;
                        }
                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "EA")           // JPE ppqq
                    {

                        if (fp == 1)
                        {
                            string jstr = mem[++PROG_CNT].ToString();
                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);
                            Mcycle += 3;
                            tstate += 10;
                        }
                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "E2")           // JPO ppqq
                    {

                        if (fp == 0)
                        {
                            string jstr = mem[++PROG_CNT].ToString();
                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);
                            Mcycle += 3;
                            tstate += 10;
                        }
                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "CA")           // JZ ppqq
                    {

                        if (fz == 1)
                        {
                            string jstr = mem[++PROG_CNT].ToString();
                            jstr = mem[++PROG_CNT].ToString() + jstr;

                            PROG_CNT = hextod(jstr);
                            Mcycle += 3;
                            tstate += 10;
                        }
                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }

                    //            Rotation             //



                    if (mem[PROG_CNT].ToString() == "17")            //  RAL  
                    {

                        string s = Convert.ToString(ra, 2);


                        int lim = s.Length;
                        lim = 8 - lim;

                        for (int i = 0; i < lim; i++)
                            s = "0" + s;


                        string t = s.Substring(0, 1);
                        s = s.Substring(1, 7) + dtohex(fc);

                        fc = hextod(t);

                        ra = Convert.ToInt32(s, 2);

                       
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();

                    }


                    if (mem[PROG_CNT].ToString() == "1F")            //  RAR  
                    {

                        string s = Convert.ToString(ra, 2);

                        int lim = s.Length;
                        lim = 8 - lim;

                        for (int i = 0; i < lim; i++)
                            s = "0" + s;

                        string t = s.Substring(7, 1);

                        s = dtohex(fc) + s.Substring(0, 7);

                        fc = hextod(t);

                        ra = Convert.ToInt32(s, 2);

                       
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }



                    if (mem[PROG_CNT].ToString() == "07")            //  RLC  
                    {


                        string s = Convert.ToString(ra, 2);

                        int lim = s.Length;
                        lim = 8 - lim;

                        for (int i = 0; i < lim; i++)
                            s = "0" + s;

                        string t = s.Substring(0, 1);

                        s = s.Substring(1, 7) + t;


                        fc = hextod(t);

                        ra = Convert.ToInt32(s, 2);

                       
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }



                    if (mem[PROG_CNT].ToString() == "0F")            //  RRC 
                    {


                        string s = Convert.ToString(ra, 2);

                        int lim = s.Length;
                        lim = 8 - lim;

                        for (int i = 0; i < lim; i++)
                            s = "0" + s;


                        string t = s.Substring(7, 1);

                        s = t + s.Substring(0, 7);


                        fc = hextod(t);

                        ra = Convert.ToInt32(s, 2);

                       
                        PROG_CNT++;
                        Mcycle += 1;
                        tstate += 4;
                        showregs();
                    }




                    //     Intrupts     //

                    if (mem[PROG_CNT].ToString() == "C7")                   //    RST 0
                    {
                        bytes = 0;
                        for (int i = 0; i < 999; i++)
                            if (mem[i].ToString() != "xx")
                                bytes++;


                        TSbox.Text = tstate.ToString();
                        Timebox.Text = (Mcycle * 2).ToString();
                        Bytebox.Text = bytes.ToString();
                        MCbox.Text = Mcycle.ToString();
                        break;
                    }

                    if (mem[PROG_CNT].ToString() == "CF")                   //    RST 1
                    {
                        bytes = 0;
                        for (int i = 0; i < 999; i++)
                            if (mem[i].ToString() != "xx")
                                bytes++;

                        PROG_CNT++;
                        showregs();

                        TSbox.Text = tstate.ToString();
                        Timebox.Text = (Mcycle * 2).ToString();
                        Bytebox.Text = bytes.ToString();
                        MCbox.Text = Mcycle.ToString();
                        Updatemem();
                        break;
                    }

                    if (mem[PROG_CNT].ToString() == "D7")                   //    RST 2
                    {
                        bytes = 0;
                        for (int i = 0; i < 999; i++)
                            if (mem[i].ToString() != "xx")
                                bytes++;


                        TSbox.Text = tstate.ToString();
                        Timebox.Text = (Mcycle * 2).ToString();
                        Bytebox.Text = bytes.ToString();
                        MCbox.Text = Mcycle.ToString();
                        break;
                    }

                    if (mem[PROG_CNT].ToString() == "DF")                   //    RST 3
                    {
                        bytes = 0;
                        for (int i = 0; i < 999; i++)
                            if (mem[i].ToString() != "xx")
                                bytes++;


                        TSbox.Text = tstate.ToString();
                        Timebox.Text = (Mcycle * 2).ToString();
                        Bytebox.Text = bytes.ToString();
                        MCbox.Text = Mcycle.ToString();
                        break;
                    }

                    if (mem[PROG_CNT].ToString() == "E7")                   //    RST 4
                    {
                        bytes = 0;
                        for (int i = 0; i < 999; i++)
                            if (mem[i].ToString() != "xx")
                                bytes++;


                        TSbox.Text = tstate.ToString();
                        Timebox.Text = (Mcycle * 2).ToString();
                        Bytebox.Text = bytes.ToString();
                        MCbox.Text = Mcycle.ToString();
                        break;
                    }

                    if (mem[PROG_CNT].ToString() == "EF")                   //    RST 5
                    {
                        bytes = 0;
                        for (int i = 0; i < 999; i++)
                            if (mem[i].ToString() != "xx")
                                bytes++;


                        TSbox.Text = tstate.ToString();
                        Timebox.Text = (Mcycle * 2).ToString();
                        Bytebox.Text = bytes.ToString();
                        MCbox.Text = Mcycle.ToString();
                        break;
                    }

                    if (mem[PROG_CNT].ToString() == "F7")                   //    RST 6
                    {
                        bytes = 0;
                        for (int i = 0; i < 999; i++)
                            if (mem[i].ToString() != "xx")
                                bytes++;


                        TSbox.Text = tstate.ToString();
                        Timebox.Text = (Mcycle * 2).ToString();
                        Bytebox.Text = bytes.ToString();
                        MCbox.Text = Mcycle.ToString();
                        break;
                    }

                    if (mem[PROG_CNT].ToString() == "FF")                   //    RST 7
                    {
                        // break;
                        ADDR.Text = "Hello FF";
                        bytes = 0;
                        for (int i = 0; i < 999; i++)
                            if (mem[i].ToString() != "xx")
                                bytes++;


                        TSbox.Text = tstate.ToString();
                        Timebox.Text = (Mcycle * 2).ToString();
                        Bytebox.Text = bytes.ToString();
                        MCbox.Text = Mcycle.ToString();
                        break;
                    }

                    if (mem[PROG_CNT].ToString() == "76")                   //    HLT
                    {
                        // break;
                        ADDR.Text = "Hello Tonu";
                        PROG_CNT++;
                        bytes = 0;
                        for (int i = 0; i < 999; i++)
                            if (mem[i].ToString() != "xx")
                                bytes++;


                        TSbox.Text = tstate.ToString();
                        Timebox.Text = (Mcycle * 2).ToString();
                        Bytebox.Text = bytes.ToString();
                        MCbox.Text = Mcycle.ToString();
                    }




                    //             Stack Related


                    if (mem[PROG_CNT].ToString() == "CD")                    //       CALL ppqq
                    {


                        PROG_CNT += 3;
                        string s = dtohex(PROG_CNT);
                        int len = 4 - s.Length;
                        for (int i = 0; i < len; i++)
                            s = "0" + s;

                        push(s.Substring(s.Length - 2, 2));
                        push(s.Substring(s.Length - 4, 2));

                        PROG_CNT -= 2;
                        string jstr = mem[PROG_CNT].ToString();

                        jstr = mem[++PROG_CNT].ToString() + jstr;
                        PROG_CNT = hextod(jstr);

                        DT.Text = dtohex(PROG_CNT);
                        Mcycle += 5;
                        tstate += 18;
                        showregs();

                    }

                    if (mem[PROG_CNT].ToString() == "C9")               //       RET      
                    {
                        string s1 = pop();
                        string s2;

                        if (s1 != "xx")
                        {
                            s2 = pop();
                            if (s2 != "xx")
                            {
                                s2 = s1 + s2;

                                PROG_CNT = hextod(s2);
                            }
                        }
                        Mcycle += 3;
                        tstate += 10;
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "DC")                    //       CC ppqq
                    {

                        if (fc == 1)
                        {
                            PROG_CNT += 3;
                            string s = dtohex(PROG_CNT);
                            int len = 4 - s.Length;
                            for (int i = 0; i < len; i++)
                                s = "0" + s;

                            push(s.Substring(s.Length - 2, 2));
                            push(s.Substring(s.Length - 4, 2));

                            PROG_CNT -= 2;
                            string jstr = mem[PROG_CNT].ToString();

                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);

                            DT.Text = dtohex(PROG_CNT);
                            Mcycle += 5;
                            tstate += 18;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 9;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "D4")                    //       CNC ppqq
                    {

                        if (fc != 1)
                        {
                            PROG_CNT += 3;
                            string s = dtohex(PROG_CNT);
                            int len = 4 - s.Length;
                            for (int i = 0; i < len; i++)
                                s = "0" + s;

                            push(s.Substring(s.Length - 2, 2));
                            push(s.Substring(s.Length - 4, 2));

                            PROG_CNT -= 2;
                            string jstr = mem[PROG_CNT].ToString();

                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);

                            DT.Text = dtohex(PROG_CNT);
                            Mcycle += 5;
                            tstate += 18;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 9;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "CC")                    //       CZ ppqq
                    {

                        if (fz == 1)
                        {
                            PROG_CNT += 3;
                            string s = dtohex(PROG_CNT);
                            int len = 4 - s.Length;
                            for (int i = 0; i < len; i++)
                                s = "0" + s;

                            push(s.Substring(s.Length - 2, 2));
                            push(s.Substring(s.Length - 4, 2));

                            PROG_CNT -= 2;
                            string jstr = mem[PROG_CNT].ToString();

                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);

                            DT.Text = dtohex(PROG_CNT);
                            Mcycle += 5;
                            tstate += 18;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 9;
                        }
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "C$")                    //       CNZ ppqq
                    {

                        if (fz != 1)
                        {
                            PROG_CNT += 3;
                            string s = dtohex(PROG_CNT);
                            int len = 4 - s.Length;
                            for (int i = 0; i < len; i++)
                                s = "0" + s;

                            push(s.Substring(s.Length - 2, 2));
                            push(s.Substring(s.Length - 4, 2));

                            PROG_CNT -= 2;
                            string jstr = mem[PROG_CNT].ToString();

                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);

                            DT.Text = dtohex(PROG_CNT);
                            Mcycle += 5;
                            tstate += 18;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 9;
                        }
                        showregs();
                    }



                    if (mem[PROG_CNT].ToString() == "FC")                    //       CM ppqq
                    {
                        if (fs == 1)

                        {
                            PROG_CNT += 3;
                            string s = dtohex(PROG_CNT);
                            int len = 4 - s.Length;
                            for (int i = 0; i < len; i++)
                                s = "0" + s;

                            push(s.Substring(s.Length - 2, 2));
                            push(s.Substring(s.Length - 4, 2));

                            PROG_CNT -= 2;
                            string jstr = mem[PROG_CNT].ToString();

                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);

                            DT.Text = dtohex(PROG_CNT);
                            Mcycle += 5;
                            tstate += 18;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 9;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "F4")                    //       CP ppqq
                    {
                        if (fs != 1)

                        {
                            PROG_CNT += 3;
                            string s = dtohex(PROG_CNT);
                            int len = 4 - s.Length;
                            for (int i = 0; i < len; i++)
                                s = "0" + s;

                            push(s.Substring(s.Length - 2, 2));
                            push(s.Substring(s.Length - 4, 2));

                            PROG_CNT -= 2;
                            string jstr = mem[PROG_CNT].ToString();

                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);

                            DT.Text = dtohex(PROG_CNT);
                            Mcycle += 5;
                            tstate += 18;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 9;
                        }
                        showregs();
                    }

                    if (mem[PROG_CNT].ToString() == "EC")                    //       CPE ppqq
                    {
                        if (fp == 1)

                        {
                            PROG_CNT += 3;
                            string s = dtohex(PROG_CNT);
                            int len = 4 - s.Length;
                            for (int i = 0; i < len; i++)
                                s = "0" + s;

                            push(s.Substring(s.Length - 2, 2));
                            push(s.Substring(s.Length - 4, 2));

                            PROG_CNT -= 2;
                            string jstr = mem[PROG_CNT].ToString();

                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);

                            DT.Text = dtohex(PROG_CNT);
                            Mcycle += 5;
                            tstate += 18;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 9;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "E4")                    //       CPO ppqq
                    {
                        if (fp != 1)

                        {
                            PROG_CNT += 3;
                            string s = dtohex(PROG_CNT);
                            int len = 4 - s.Length;
                            for (int i = 0; i < len; i++)
                                s = "0" + s;

                            push(s.Substring(s.Length - 2, 2));
                            push(s.Substring(s.Length - 4, 2));

                            PROG_CNT -= 2;
                            string jstr = mem[PROG_CNT].ToString();

                            jstr = mem[++PROG_CNT].ToString() + jstr;
                            PROG_CNT = hextod(jstr);

                            DT.Text = dtohex(PROG_CNT);
                            Mcycle += 5;
                            tstate += 18;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 9;
                        }
                        showregs();
                    }



                    if (mem[PROG_CNT].ToString() == "F8")               //       RM      
                    {

                        if (fs == 1)
                        {
                            string s1 = pop();
                            string s2;

                            if (s1 != "xx")
                            {
                                s2 = pop();
                                if (s2 != "xx")
                                {
                                    s2 = s1 + s2;

                                    PROG_CNT = hextod(s2);
                                }
                            }
                            Mcycle += 3;
                            tstate += 10;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();

                    }


                    if (mem[PROG_CNT].ToString() == "D0")               //       RNC     
                    {

                        if (fc == 0)
                        {
                            string s1 = pop();
                            string s2;

                            if (s1 != "xx")
                            {
                                s2 = pop();
                                if (s2 != "xx")
                                {
                                    s2 = s1 + s2;

                                    PROG_CNT = hextod(s2);
                                }
                            }
                            Mcycle += 3;
                            tstate += 10;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "D8")               //       RC     
                    {

                        if (fc != 0)
                        {
                            string s1 = pop();
                            string s2;

                            if (s1 != "xx")
                            {
                                s2 = pop();
                                if (s2 != "xx")
                                {
                                    s2 = s1 + s2;

                                    PROG_CNT = hextod(s2);
                                }
                            }
                            Mcycle += 3;
                            tstate += 10;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "C8")               //       RZ      
                    {

                        if (fz != 0)
                        {
                            string s1 = pop();
                            string s2;

                            if (s1 != "xx")
                            {
                                s2 = pop();
                                if (s2 != "xx")
                                {
                                    s2 = s1 + s2;

                                    PROG_CNT = hextod(s2);
                                }
                            }
                            Mcycle += 3;
                            tstate += 10;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "C0")               //       RNZ      
                    {

                        if (fz == 0)
                        {
                            string s1 = pop();
                            string s2;

                            if (s1 != "xx")
                            {
                                s2 = pop();
                                if (s2 != "xx")
                                {
                                    s2 = s1 + s2;

                                    PROG_CNT = hextod(s2);
                                }
                            }
                            Mcycle += 3;
                            tstate += 10;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "F0")               //       RP      
                    {

                        if (fs == 0)
                        {
                            string s1 = pop();
                            string s2;

                            if (s1 != "xx")
                            {
                                s2 = pop();
                                if (s2 != "xx")
                                {
                                    s2 = s1 + s2;

                                    PROG_CNT = hextod(s2);
                                }
                            }
                            Mcycle += 3;
                            tstate += 10;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();

                    }

                    if (mem[PROG_CNT].ToString() == "E8")               //       RPE      
                    {

                        if (fp == 1)
                        {
                            string s1 = pop();
                            string s2;

                            if (s1 != "xx")
                            {
                                s2 = pop();
                                if (s2 != "xx")
                                {
                                    s2 = s1 + s2;

                                    PROG_CNT = hextod(s2);
                                }
                            }
                            Mcycle += 3;
                            tstate += 10;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();

                    }


                    if (mem[PROG_CNT].ToString() == "EO")               //       RPO      
                    {

                        if (fs == 0)
                        {
                            string s1 = pop();
                            string s2;

                            if (s1 != "xx")
                            {
                                s2 = pop();
                                if (s2 != "xx")
                                {
                                    s2 = s1 + s2;

                                    PROG_CNT = hextod(s2);
                                }
                            }
                            Mcycle += 3;
                            tstate += 10;
                        }

                        else
                        {
                            PROG_CNT += 2;
                            Mcycle += 2;
                            tstate += 7;
                        }
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "E9")                //      PCHL
                    {
                        PROG_CNT = hextod(dtohex(rh) + dtohex(rl));
                        Mcycle += 1;
                        tstate += 6;
                        showregs();
                    }


                    if (mem[PROG_CNT].ToString() == "C1")               //       POP B      
                    {

                        rc = hextod(mem[STK_PNT++].ToString());
                        rd = hextod(mem[STK_PNT++].ToString());

                        PROG_CNT++;
                        Mcycle += 3;
                        tstate += 10;
                        showregs();

                    }

                    if (mem[PROG_CNT].ToString() == "D1")               //       POP D     
                    {

                        re = hextod(mem[STK_PNT++].ToString());
                        rd = hextod(mem[STK_PNT++].ToString());

                        PROG_CNT++;
                        Mcycle += 3;
                        tstate += 10;
                        showregs();

                    }

                    if (mem[PROG_CNT].ToString() == "E1")               //       POP H     
                    {

                        rl = hextod(mem[STK_PNT++].ToString());
                        rh = hextod(mem[STK_PNT++].ToString());

                        PROG_CNT++;
                        Mcycle += 3;
                        tstate += 10;
                        showregs();

                    }

                    if (mem[PROG_CNT].ToString() == "F1")               //       POP PSW    
                    {

                        string s = mem[STK_PNT++].ToString();

                        ra = hextod(mem[STK_PNT++].ToString());

                        string bin = Convert.ToString(hextod(s), 2);

                        int lim = bin.Length;
                        lim = 8 - lim;

                        for (int i = 0; i < lim; i++)
                            bin = "0" + bin;


                        fs = Convert.ToInt32(bin.Substring(0, 1));
                        fz = Convert.ToInt32(bin.Substring(1, 1));

                        fac = Convert.ToInt32(bin.Substring(3, 1));

                        fp = Convert.ToInt32(bin.Substring(5, 1));

                        fc = Convert.ToInt32(bin.Substring(7, 1));

                      

                        Mcycle += 3;

                        tstate += 12;


                        PROG_CNT++;
                        showregs();
                    }



                    if (mem[PROG_CNT].ToString() == "C5")               //       PUSH B     
                    {

                        string s1 = dtohex(rb);
                        string s2 = dtohex(rc);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;


                        if (STK_PNT != 0)
                        {
                            mem[STK_PNT] = s1;
                            mem[--STK_PNT] = s2;
                        }
                        else
                            GO_tb.Text = "Stack Is Not Initialized !!!";

                        PROG_CNT++;
                        Mcycle += 3;
                        tstate += 12;

                        showregs();
    
                    }


                    if (mem[PROG_CNT].ToString() == "D5")               //       PUSH D     
                    {

                        string s1 = dtohex(rd);
                        string s2 = dtohex(re);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        if (STK_PNT != 0)
                        {
                            mem[STK_PNT] = s1;
                            mem[--STK_PNT] = s2;
                        }
                        else
                            GO_tb.Text = "Stack Is Not Initialized !!!";

                        PROG_CNT++;
                        Mcycle += 3;
                        tstate += 12;

                        showregs();
                      
                    }

                    if (mem[PROG_CNT].ToString() == "E5")               //       PUSH H     
                    {

                        string s1 = dtohex(rh);
                        string s2 = dtohex(rl);

                        if (s1.Length == 1)
                            s1 = "0" + s1;
                        if (s2.Length == 1)
                            s2 = "0" + s2;

                        if (STK_PNT != 0)
                        {
                            mem[STK_PNT] = s1;
                            mem[--STK_PNT] = s2;
                        }
                        else
                            GO_tb.Text = "Stack Is Not Initialized !!!";


                        PROG_CNT++;
                        Mcycle += 3;
                        tstate += 12;

                        showregs();
                       
                    }

                    if (mem[PROG_CNT].ToString() == "F5")               //       PUSH PSW     
                    {

                        if (STK_PNT != 0)
                        {
                            string o = dtohex(ra);

                            if (o.Length == 1)
                                o = "0" + o;

                            mem[STK_PNT--] = o;

                            string lv = fs.ToString() + fz.ToString() + "0" + fac.ToString() + "0" + fp.ToString() + "0" + fc.ToString();

                            string oo = dtohex(Convert.ToInt32(lv, 2));

                            if (oo.Length == 1)
                                oo = "0" + oo;

                            mem[STK_PNT] = oo;
                            PROG_CNT++;
                            showregs();

                            Mcycle += 3;
                            tstate += 10;
                        }

                        else
                            GO_tb.Text = "Stack Is Not Initialized !!!";
                    }

                    //   else if (mem[PROG_CNT].ToString() == "xx")
                    //  {
                    //      GO_tb.Text = "Invalid Data or Code Encountered !!!";   
                    //   }


                    //  jmpllb: GO_tb.Text = "Invalid Data or Code Encountered !!!";

                    bytes = 0;
                    for (int i = 0; i < 999; i++)
                        if (mem[i].ToString() != "xx")
                            bytes++;


                    TSbox.Text = tstate.ToString();
                    Timebox.Text = (Mcycle * 2).ToString();
                    Bytebox.Text = bytes.ToString();
                    MCbox.Text = Mcycle.ToString();

                }
            }

         
            //  ADDR.Text = "END";
        }


    }

}

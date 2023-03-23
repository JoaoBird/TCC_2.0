﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace teste
{
    public partial class cursos_pg : Form
    {
        conexao con = new conexao();
        public cursos_pg()
        {
            InitializeComponent();
        }
        public static void abre_home()
        {

            Application.Run(new Form1());
        }

        public static void abre_cursos()
        {

            Application.Run(new cursos_pg());
        }
        
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(abre_home));
            t.Start();
            this.Close();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(abre_cursos));
            t.Start();
            this.Close();
        }
        public static void Abre_tela_c(int id_curso)
        {

            tela_c cursos_exibidos = new tela_c(id_curso);
            cursos_exibidos.ShowDialog();

        }

        private void cursos_pg_Load(object sender, EventArgs e)
        {
            MySqlConnection Conexao = con.getconexao();// chama a conexão mysql
            Conexao.Open();//abre conexao
            string query = "select id_curso,nome_curso,preco,tb_tipo_curso.tipo_curso from tb_curso inner join tb_tipo_curso on tb_tipo_curso.id_tipo_curso=tb_curso.id_tipo_curso  ";//nome da consulta
            MySqlCommand comando = new MySqlCommand(query, Conexao);//comando sql para montar

            MySqlDataReader registro = comando.ExecuteReader();//ler os dados da consulta




            while (registro.Read())//ler 1 registro
            {
                Panel cursos = new Panel()
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    AutoSize = true

                };

                Label nome_curso = new Label()
             
                {
                  AutoSize=true

                };
                Label preco = new Label()

                {
                    AutoSize = true,


            };
                Label tipo_curso = new Label()

                {
                    AutoSize = true

                };

                Button curso_pg = new Button()

                {
                    AutoSize = true

                };


                nome_curso.Text = registro.GetString("nome_curso");

                cursos.Click += new EventHandler((sender1,e1)=> cursosClick(sender1 ,e1, registro.GetInt32("id_curso")));
            
                //cursos.Click += Abre_tela_c();

               





                preco.Text = Convert.ToString(registro.GetDecimal("preco"));
                tipo_curso.Text = registro.GetString("tipo_curso");
                cursos.Controls.Add(nome_curso);

                espaco(preco,25);
                //preco.Location = new Point((int)preco.Location.X, (int)(preco.Location.Y - 50));
               
                cursos.Controls.Add(preco);
          
                cursos.Controls.Add(tipo_curso);
                espaco(tipo_curso,50);
                painel_r.Controls.Add(cursos);

            }

            
            
            Conexao.Close();
        }

        private void cursosClick(object sender, EventArgs e, int id_curso)
        {

            Abre_tela_c(id_curso);



        }

        public void espaco(Label label,int pos)
        {
            label.Location = new Point(label.Location.X, (label.Location.Y + pos));
            label.Refresh();
        }

    }
    }


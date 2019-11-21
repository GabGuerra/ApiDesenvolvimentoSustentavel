using ApiApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApp.Business
{
    public class EventoBL
    {
        public void CadastrarEvento(EventoModel evento)
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();
            try
            {
                conn.Open();
                string strSql = @"INSERT INTO EVENTO(DSC_EVENTO, QTD_INTEGRANTES, QTD_RECOMENDADA_INTEGRANTES, IND_SITUACAO_EVENTO, DAT_INICIO_EVENTO, DAT_FIM_EVENTO, DSC_CATEGORIA_EVENTO)
                                      VALUES
                                      (@DSCEVENTO, 1, @QTDRECOMENDADA, @INDSITUACAO, @DATINICIO, @DATFIM, @DSCCATEGORIA);";

                MySqlCommand sql = new MySqlCommand(strSql, conn);
                sql.Parameters.AddWithValue("@DSCEVENTO", evento.DscEvento);
                sql.Parameters.AddWithValue("@QTDRECOMENDADA", evento.QtdRecomendadaIntegrantes);
                sql.Parameters.AddWithValue("@INDSITUACAO", evento.IndSituacaoEvento);
                sql.Parameters.AddWithValue("@DATINICIO", Convert.ToDateTime(evento.DatInicioEvento));
                sql.Parameters.AddWithValue("@DATFIM", Convert.ToDateTime(evento.DatFimEvento));
                sql.Parameters.AddWithValue("@DSCCATEGORIA", evento.DscCategoriaEvento);
                sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
           //Busca Eventos Destaque
        public EventoModel BuscaEventoDestaque()
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();           
            try
            {
                conn.Open();
                string strSql = @"SELECT * FROM EVENTO WHERE IND_EVENTO_DESTAQUE = 1";
                MySqlCommand sql = new MySqlCommand(strSql, conn);
                var dr = sql.ExecuteReader();
                EventoModel eventoDestaque = new EventoModel();
                while (dr.Read())
                {
                    eventoDestaque.CodEvento = Convert.ToInt32(dr["cod_evento"]);
                    eventoDestaque.DscEvento = dr["DSC_EVENTO"].ToString();
                    eventoDestaque.DatInicioEvento = Convert.ToDateTime(dr["DAT_INICIO_EVENTO"]);
                    eventoDestaque.DscCategoriaEvento = dr["DSC_CATEGORIA_EVENTO"].ToString();
                }
                return eventoDestaque;
            }
            catch
            {

                return null;
            }
            finally
            {
                conn.Close();

            }
        }


        //grupos recentes

        public List<EventoModel> BuscaGruposRecentes()
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();
        
            try
            {
                List<EventoModel> ListaEvento = new List<EventoModel>();
                conn.Open();
                string strSql = @"SELECT * FROM EVENTO WHERE IND_EVENTO_DESTAQUE = 0";
                MySqlCommand sql = new MySqlCommand(strSql, conn);
                var dr = sql.ExecuteReader();
               
                while (dr.Read())
                {
                    EventoModel eventoDestaque = new EventoModel();
                    eventoDestaque.CodEvento = Convert.ToInt32(dr["cod_evento"]);
                    eventoDestaque.CodEvento.ToString();
                    eventoDestaque.DscEvento = dr["DSC_EVENTO"].ToString();
                    eventoDestaque.DatInicioEvento = Convert.ToDateTime(dr["DAT_INICIO_EVENTO"]);
                    eventoDestaque.DscCategoriaEvento = dr["DSC_CATEGORIA_EVENTO"].ToString();
                    ListaEvento.Add(eventoDestaque);
                }
                return ListaEvento;
            }
            catch
            {

                return null;
            }
            finally
            {
                conn.Close();

            }
        }

        //Busca dados do evento clicado
        public EventoModel  BuscaEvento(int codEvento)
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();

            try
            {
                conn.Open();
                string strSql = @"SELECT * FROM EVENTO WHERE COD_EVENTO = @CODEVENTO";
                
                MySqlCommand sql = new MySqlCommand(strSql, conn);
                sql.Parameters.AddWithValue("@CODEVENTO", codEvento);
                var dr = sql.ExecuteReader();
                EventoModel eventoDestaque = new EventoModel();
                while (dr.Read())
                {                    
                    eventoDestaque.CodEvento = Convert.ToInt32(dr["cod_evento"]);
                    eventoDestaque.CodEvento.ToString();
                    eventoDestaque.DscEvento = dr["DSC_EVENTO"].ToString();
                    eventoDestaque.DatInicioEvento = Convert.ToDateTime(dr["DAT_INICIO_EVENTO"]);
                    eventoDestaque.DscCategoriaEvento = dr["DSC_CATEGORIA_EVENTO"].ToString();
                }
                return eventoDestaque;
            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
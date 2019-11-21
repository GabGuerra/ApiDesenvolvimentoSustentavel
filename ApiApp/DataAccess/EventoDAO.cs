using ApiApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiApp.DataAccess
{
    public class EventoDAO
    {

        private DataTable dt;

        public EventoDAO()
        {
            dt = new DataTable();
        }
        /// <summary>Método para registrar dados de evento                
        /// </summary>
        public void CadastrarEvento(CadEventoModel evento) 
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
        /// <summary>Método para buscar dados de evento destaque
        /// </summary>
        public DataTable BuscaEventoDestaque() 
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();
            try
            {
                conn.Open();
                string strSql = @"SELECT 
                                    E.COD_EVENTO,
	                                E.DSC_EVENTO ,
	                                E.QTD_INTEGRANTES ,
	                                E.QTD_RECOMENDADA_INTEGRANTES,
	                                E.IND_SITUACAO_EVENTO ,
	                                E.DAT_INICIO_EVENTO ,
	                                E.DAT_FIM_EVENTO ,
	                                E.COD_CATEGORIA_EVENTO 
                                FROM EVENTO E
                                INNER JOIN EVENTO_CATEGORIA EC ON E.COD_CATEGORIA_EVENTO = EC.COD_CATEGORIA
                                WHERE E.IND_EVENTO_DESTAQUE = TRUE;";
                MySqlCommand sql = new MySqlCommand(strSql, conn);
                dt.Load(sql.ExecuteReader());
                return dt;                
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
    }
}
using ApiApp.DataAccess;
using ApiApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiApp.Business
{
    public class EventoBL: IDisposable
    {
        private EventoDAO dao;

        public EventoBL()
        {
            dao = new EventoDAO();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public void CadastrarEvento(CadEventoModel evento)
        {            
            try
            {
                dao.CadastrarEvento(evento);       
            }
            catch (Exception ex)
            {
                //Error.Message = $"Erro ao cadastrar evento: {ex.Message}"
                throw ex;
            }          
        }
         //Busca Eventos Destaque
        public CadEventoModel BuscaEventoDestaque()
        {
            CadEventoModel eventoDestaque = new CadEventoModel();
            try
            {
                using (var dt = dao.BuscaEventoDestaque())
                {
                    try
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {                                
                                eventoDestaque.Cod_Evento = Convert.ToInt32(dr["COD_EVENTO"]);
                                eventoDestaque.DscEvento = dr["DSC_EVENTO"].ToString();
                            }                           
                        }                        
                    }
                    catch
                    {
                        //Error.Message = $"Erro ao buscar evento destaque: {ex.Message}"
                        throw;
                    }
                    finally
                    {
                        dt.Clear();
                    }
                }               
            }
            catch(Exception ex)
            {

                throw ex;
            }
            return eventoDestaque;
        }      
    }
}
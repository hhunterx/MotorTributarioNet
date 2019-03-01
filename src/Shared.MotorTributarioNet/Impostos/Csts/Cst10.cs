﻿//                      Projeto: Motor Tributario                                                  
//          Biblioteca C# para Cálculos Tributários Do Brasil
//                    NF-e, NFC-e, CT-e, SAT-Fiscal     
//                                                                                                                                           
//  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la 
// sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  
// Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) 
// qualquer versão posterior.                                                   
//                                                                              
//  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   
// NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      
// ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor
// do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              
//                                                                              
//  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto
// com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  
// no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          
// Você também pode obter uma copia da licença em:                              
// https://github.com/AutomacaoNet/MotorTributarioNet/blob/master/LICENSE      

using MotorTributarioNet.Facade;
using MotorTributarioNet.Flags;

namespace MotorTributarioNet.Impostos.Csts
{
    public class Cst10 : Cst00
    {
        public ModalidadeDeterminacaoBcIcmsSt ModalidadeDeterminacaoBcIcmsSt { get; set; }

        public decimal PercentualMva { get; private set; }
        public decimal PercentualReducaoSt { get; private set; }
        public decimal ValorBcIcmsSt { get; private set; }
        public decimal PercentualIcmsSt { get; private set; }
        public decimal ValorIcmsSt { get; private set; }
		public decimal ValorBcFcp { get; private set; }
		public decimal ValorBcFcpSt { get; private set; }
		public decimal PercentualFcpSt { get; private set; }
		public decimal ValorFcpSt { get; private set; }

		public Cst10(OrigemMercadoria origemMercadoria = OrigemMercadoria.Nacional, TipoDesconto tipoDesconto = TipoDesconto.Incondicional) : base(origemMercadoria, tipoDesconto)
        {
            Cst = Cst.Cst10;
            ModalidadeDeterminacaoBcIcmsSt = ModalidadeDeterminacaoBcIcmsSt.MargemValorAgregado;
        }

        public override void Calcula(ITributavel tributavel)
        {
            base.Calcula(tributavel);

            PercentualMva = tributavel.PercentualMva;
            PercentualReducaoSt = tributavel.PercentualReducaoSt;
            PercentualIcmsSt = tributavel.PercentualIcmsSt;
			PercentualFcpSt = tributavel.PercentualFcpSt;

            FacadeCalculadoraTributacao facadeCalculadoraTributacao = new FacadeCalculadoraTributacao(tributavel, TipoDesconto);

            tributavel.ValorIpi = facadeCalculadoraTributacao.CalculaIpi().Valor;

            IResultadoCalculoIcmsSt resultadoCalculoIcmsSt = facadeCalculadoraTributacao.CalculaIcmsSt();
			IResultadoCalculoFcpSt resultadoCalculoFcpSt = facadeCalculadoraTributacao.CalculaFcpSt();

            ValorBcIcmsSt = resultadoCalculoIcmsSt.BaseCalculoIcmsSt;
            ValorIcmsSt = resultadoCalculoIcmsSt.ValorIcmsSt;

			ValorBcFcp = facadeCalculadoraTributacao.CalculaFcp().BaseCalculo;
			ValorBcFcpSt = resultadoCalculoFcpSt.BaseCalculoFcpSt;
			ValorFcpSt = resultadoCalculoFcpSt.ValorFcpSt;
        }
    }
}

using ProvaAdmissionalCSharpApisul;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaApisul
{
    
    public class ListaAcessoElevador : List<AcessoElevador>, IElevadorService
    {
        
        public List<int> andarMenosUtilizado()
        {
            var lista = new List<int>();

            var result = _andarMenosUtilizado();

            foreach (var item in result)
            {
                lista.Add(item.id);
            }
            return lista;
        }

        
        public List<int> andarMenosUtilizado(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("Valor inválido; Esperado > 0", nameof(quantidade));
            }

            var lista = new List<int>();
            
            var result = _andarMenosUtilizado();

            foreach (var item in result)
            {
                lista.Add(item.id);
                if (lista.Count >= quantidade)
                    break;
            }

            return lista;
        }

        private  IEnumerable<dynamic> _andarMenosUtilizado()
        {

            var result = this.GroupBy(ae => ae.Andar)
                               .OrderBy(ae => ae.Key)
                               .Select(n => 
                               new {
                                    id = n.Key,
                                    qt = n.Count()
                                   });

            var result1 = result.OrderBy(ae => ae.qt);

            return result1;
        }

       
        public List<char> elevadorMaisFrequentado()
        {
            var lista = new List<char>();
            var result = _elevadorFrequencia(true );

            foreach (var item in result)
            {
                lista.Add(item.id);
            }
            return lista;
        }

        
        public List<char> elevadorMaisFrequentado(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("Valor inválido; Esperado > 0", nameof(quantidade));
            }

            var lista = new List<char>();
            var result = _elevadorFrequencia(true);

            foreach (var item in result)
            {
                lista.Add(item.id);
                if (lista.Count >= quantidade)
                    break;
            }
            return lista;
        }

        private IEnumerable<dynamic> _elevadorFrequencia(bool maiorFrequencia)
        {
            var result = this.GroupBy(ae => ae.Elevador)
                              .OrderBy(ae => ae.Key)
                              .Select(n => 
                              new {
                                   id = n.Key,
                                   qt = n.Count()
                                  });
            
            IEnumerable<dynamic> result1;
            if (maiorFrequencia)
            {
                result1 = result.OrderByDescending(ae => ae.qt);
            }
            else
            {
                result1 = result.OrderBy(ae => ae.qt);
            }

            return result1;
        }

       
        public List<char> elevadorMenosFrequentado()
        {
            var lista = new List<char>();
            var result = _elevadorFrequencia(false);

            foreach (var item in result)
            {
                lista.Add(item.id);
            }
            return lista;
        }

       
        public List<char> elevadorMenosFrequentado(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("Valor inválido; Esperado > 0", nameof(quantidade));
            }
            var lista = new List<char>();
            var result = _elevadorFrequencia(false);

            foreach (var item in result)
            {
                lista.Add(item.id);
                if (lista.Count >= quantidade)
                    break;
            }
            return lista;
        }

        
        public float percentualDeUsoElevadorA()
        {
            float percUso = _percentualDeUsoElevador('A');

            return percUso;
        }

        
        public float percentualDeUsoElevadorB()
        {
            float percUso = _percentualDeUsoElevador('B');

            return percUso;
        }

        
        public float percentualDeUsoElevadorC()
        {
            float percUso = _percentualDeUsoElevador('C');

            return percUso;
        }

        
        public float percentualDeUsoElevadorD()
        {
            float percUso = _percentualDeUsoElevador('D');

            return percUso;
        }

       
        public float percentualDeUsoElevadorE()
        {
            float percUso = _percentualDeUsoElevador('E');

            return percUso;
        }

        private float _percentualDeUsoElevador(char elevador)
        {
            float percUso;
            float quantiE = 0;
            var result = this.Where(ae => ae.Elevador == elevador )
                            .GroupBy(ae => ae.Elevador)
                            .OrderBy(ae => ae.Key)
                            .Select(n =>
                            new {
                                id = n.Key,
                                qt = n.Count()
                            });

            foreach (var item in result)
            {
                quantiE += item.qt;
            }

            float quantiT = quantiE;

            var result1 = this.Where(ae => ae.Elevador != elevador)
                        .GroupBy(ae => ae.Elevador)
                        .OrderBy(ae => ae.Key)
                        .Select(n =>
                        new {
                            id = n.Key,
                            qt = n.Count()
                        });

            foreach (var item in result1)
            {
                quantiT += item.qt;
            }

            percUso =  (100 * quantiE) / quantiT;

            return percUso;

        }

       
        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            var lista = new List<char>();
            IEnumerable<dynamic> result1;
            var result = elevadorMaisFrequentado(2);

            foreach (var item0 in result)
            {
                result1 = _periodoUtilizacao(true, item0);

                foreach (var item in result1)
                {
                    lista.Add(item.id );
                    break;
                }
            }

            return lista;
        }

       
        public List<char> periodoMaiorFluxoElevador(char elevador)
        {
            var lista = new List<char>();
            var result1 = _periodoUtilizacao(true, elevador);

            foreach (var item in result1)
            {
                lista.Add(item.id);
                
            }

            return lista;
        }
       
        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            var lista = new List<char>();
            var result = _periodoUtilizacao(true, ' ');

            foreach (var item in result)
            {
                lista.Add(item.id);
            }
            return lista;
        }
       
        public List<char> periodoMaiorUtilizacaoConjuntoElevadores(int quantidade)
        {
            var lista = new List<char>();
            var result = _periodoUtilizacao(true, ' ');

            foreach (var item in result)
            {
                lista.Add(item.id);
                if (lista.Count >= quantidade)
                    break;
            }
            return lista;
        }
        private IEnumerable<dynamic> _periodoUtilizacao(bool maiorFluxo, char elevador)
        {

            IEnumerable<dynamic> result;
            if (elevador != ' ')
            {
                result = this.Where(ae => ae.Elevador == elevador)
                            .GroupBy(ae => ae.Turno)
                            .OrderBy(ae => ae.Key)
                            .Select(n =>
                             new {
                                   id = n.Key,
                                   qt = n.Count()
                                 });
            }
            else
            {
                result = this.GroupBy(ae => ae.Turno)
                             .OrderBy(ae => ae.Key)
                             .Select(n =>
                             new {
                                 id = n.Key,
                                 qt = n.Count()
                             });
            }

            IEnumerable<dynamic> result1;
            if (maiorFluxo)
            {
                result1 = result.OrderByDescending(ae => ae.qt);
            }
            else
            {
                result1 = result.OrderBy(ae => ae.qt);
            }

            return result1;
        }

       
        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            var lista = new List<char>();
            IEnumerable<dynamic> result1;
            var result = elevadorMenosFrequentado(2);

            foreach (var item0 in result)
            {
                result1 = _periodoUtilizacao(false, item0);

                foreach (var item in result1)
                {
                    lista.Add(item.id);
                    break;
                }
            }

            return lista;
        }
       
        public List<char> periodoMenorFluxoElevador(char elevador)
        {
            var lista = new List<char>();
            var result1 = _periodoUtilizacao(false, elevador);

            foreach (var item in result1)
            {
                lista.Add(item.id);
                break;
            }

            return lista;
        }


    }

}

using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace ProjetoMovimento.DAL.LinqToSQL
{
    [Database(Name = "MovimentoManual")]
    public class MovimentoDataContext : DataContext
    {
        public MovimentoDataContext(string connection) : base(connection) { }
        public Table<MovimentoManual> Movimentos;
        public Table<Produto> Produtos;
        public Table<ProdutoCosif> ProdutoCosifs;
    }

    [Table(Name = "MOVIMENTO_MANUAL")]
    public class MovimentoManual
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column] public int DAT_MES { get; set; }
        [Column] public int DAT_ANO { get; set; }
        [Column] public string DES_DESCRICAO { get; set; }
        [Column] public decimal VAL_VALOR { get; set; }
        [Column] public string COD_PRODUTO { get; set; }
        [Column] public string COD_COSIF { get; set; }
    }

    [Table(Name = "PRODUTO")]
    public class Produto
    {
        [Column(IsPrimaryKey = true)] public string COD_PRODUTO { get; set; }
        [Column] public string DES_PRODUTO { get; set; }
        [Column] public string STA_STATUS { get; set; }
    }

    [Table(Name = "PRODUTO_COSIF")]
    public class ProdutoCosif
    {
        [Column(IsPrimaryKey = true)] public string COD_COSIF { get; set; }
        [Column] public string COD_PRODUTO { get; set; }
        [Column] public string COD_CLASSIFICACAO { get; set; }
        [Column] public string STA_STATUS { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class Movement
    {
        public int MovementID { get; set; }

        public int? DocumentID { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? Discount { get; set; }

        [Column(TypeName = "money")]
        public decimal? PurchasePrice { get; set; }

        [Column(TypeName = "money")]
        public decimal? SalePrice { get; set; }

        public int? IsFoil { get; set; }
        public int? IsSigned { get; set; }
        public int? IsAltered { get; set; }
        public int? IsPlayset { get; set; }

        [ForeignKey("FK_Movements_ProductID_Products")]
        public int? ProductID { get; set; }
        public virtual Product product { get; set; }

        [ForeignKey("FK_Movements_DocumentTypeID_DocumentTypes")]
        public int? DocumentTypeID { get; set; }
        public virtual DocumentType documentType { get; set; }

        [ForeignKey("FK_Movements_ConditionID_Conditions")]
        public int? ConditionID { get; set; }
        public virtual Condition condition { get; set; }

        [ForeignKey("FK_Movements_StoreID_Stores")]
        public int StoreID { get; set; }
        public virtual Store store { get; set; }

        public virtual List<MovementTax> movementsTaxes { get; set; }

        public Movement()
        {

        }

        public Movement(Movement movement)
        {
            this.Discount = movement.Discount;
            this.MovementID = movement.MovementID;
            this.DocumentID = movement.DocumentID;
            this.DocumentTypeID = movement.DocumentTypeID;
            this.documentType = movement.documentType;
            this.Quantity = movement.Quantity;
            this.PurchasePrice = movement.PurchasePrice;
            this.SalePrice = movement.SalePrice;
            this.product = movement.product;
            this.ProductID = movement.ProductID;
            this.condition = movement.condition;
            this.ConditionID = movement.ConditionID;
            this.store = movement.store;
            this.StoreID = movement.StoreID;

            this.IsAltered = movement.IsAltered;
            this.IsFoil = movement.IsFoil;
            this.IsPlayset = movement.IsPlayset;
            this.IsSigned = movement.IsSigned;
        }
    }
}

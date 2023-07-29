using Services.Domains.ReportCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImportExport.Export
{
	public class ExcelExport
	{
		#region ctor
		public ExcelExport()
		{

		}
		#endregion

		public string FilePath { get; private set; }

		public void Export(Sheet? sheet)
		{
			if (sheet == null)
				throw new ArgumentNullException(nameof(sheet));


		}
	}
}

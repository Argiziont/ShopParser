using ShopParserApi.Services.Dapper_Services;
using ShopParserApi.Services.Dapper_Services.Interfaces;

namespace ShopParserApi.Services.GeneratedClientFile
{

	#region sp_TempTableTest
	//Couldn't parse Stored procedure  with className: sp_TempTableTest because of internal error: The metadata could not be determined because statement 'SELECT * FROM #T122212' in procedure 'sp_TempTableTest' uses a temp table.

	#endregion



	#region Sp_CountProductsWithCategory
	public class Sp_CountProductsWithCategoryOutput
	{
		[Newtonsoft.Json.JsonProperty("Result", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 Result { get; set; }


	}

	public class Sp_CountProductsWithCategoryInput
	{
		[Newtonsoft.Json.JsonProperty("categoryId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CategoryId { get; set; }


	}

	public class Sp_CountProductsWithCategory
	{
		private readonly IDapperExecutor<Sp_CountProductsWithCategoryInput, Sp_CountProductsWithCategoryOutput> _dapperExecutor;

		public Sp_CountProductsWithCategory(IDapperExecutor<Sp_CountProductsWithCategoryInput, Sp_CountProductsWithCategoryOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_CountProductsWithCategoryOutput>> Execute(Sp_CountProductsWithCategoryInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_CountProductsWithCategory", request);
		}


	}


	#endregion



	#region Sp_GetAllCategories
	public class Sp_GetAllCategoriesOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 SupCategoryDataId { get; set; }


	}


	public class Sp_GetAllCategories
	{
		private readonly IDapperExecutor<EmptyInputParams, Sp_GetAllCategoriesOutput> _dapperExecutor;

		public Sp_GetAllCategories(IDapperExecutor<EmptyInputParams, Sp_GetAllCategoriesOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetAllCategoriesOutput>> Execute()
		{
			return _dapperExecutor.ExecuteAsync("Sp_GetAllCategories");
		}


	}


	#endregion



	#region Sp_GetAllProducts
	[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.4.0 (Newtonsoft.Json v13.0.0.0)")]
	public partial class Sp_GetAllProductsOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Always)]
		public int Id { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Title", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Title { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate", Required = Newtonsoft.Json.Required.Always)]
		[System.ComponentModel.DataAnnotations.Required]
		public System.DateTimeOffset SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ExpirationDate", Required = Newtonsoft.Json.Required.Always)]
		[System.ComponentModel.DataAnnotations.Required]
		public System.DateTimeOffset ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ProductState", Required = Newtonsoft.Json.Required.Always)]
		public int ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("Description", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Description { get; set; }

		[Newtonsoft.Json.JsonProperty("Price", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Price { get; set; }

		[Newtonsoft.Json.JsonProperty("KeyWords", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string JsonDataSchema { get; set; }


	}

	public class Sp_GetAllProducts
	{
		private readonly IDapperExecutor<EmptyInputParams, Sp_GetAllProductsOutput> _dapperExecutor;

		public Sp_GetAllProducts(IDapperExecutor<EmptyInputParams, Sp_GetAllProductsOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetAllProductsOutput>> Execute()
		{
			return _dapperExecutor.ExecuteJsonAsync("Sp_GetAllProducts");
		}


	}


	#endregion



	#region Sp_GetNestedCategoryByParentId
	[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.4.0 (Newtonsoft.Json v13.0.0.0)")]
	public partial class Sp_GetNestedCategoryByParentIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int SupCategoryDataId { get; set; }


	}
	public class Sp_GetNestedCategoryByParentIdInput
	{
		[Newtonsoft.Json.JsonProperty("categoryId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CategoryId { get; set; }


	}

	public class Sp_GetNestedCategoryByParentId
	{
		private readonly IDapperExecutor<Sp_GetNestedCategoryByParentIdInput, Sp_GetNestedCategoryByParentIdOutput> _dapperExecutor;

		public Sp_GetNestedCategoryByParentId(IDapperExecutor<Sp_GetNestedCategoryByParentIdInput, Sp_GetNestedCategoryByParentIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetNestedCategoryByParentIdOutput>> Execute(Sp_GetNestedCategoryByParentIdInput request)
		{
			return _dapperExecutor.ExecuteJsonAsync("Sp_GetNestedCategoryByParentId", request);
		}


	}


	#endregion



	#region Sp_GetNestedCategoryByParentIdAndCompanyId
	[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.4.0 (Newtonsoft.Json v13.0.0.0)")]
	public partial class Sp_GetNestedCategoryByParentIdAndCompanyIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int SupCategoryDataId { get; set; }


	}
	public class Sp_GetNestedCategoryByParentIdAndCompanyIdInput
	{
		[Newtonsoft.Json.JsonProperty("categoryId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CategoryId { get; set; }

		[Newtonsoft.Json.JsonProperty("companyId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CompanyId { get; set; }


	}

	public class Sp_GetNestedCategoryByParentIdAndCompanyId
	{
		private readonly IDapperExecutor<Sp_GetNestedCategoryByParentIdAndCompanyIdInput, Sp_GetNestedCategoryByParentIdAndCompanyIdOutput> _dapperExecutor;

		public Sp_GetNestedCategoryByParentIdAndCompanyId(IDapperExecutor<Sp_GetNestedCategoryByParentIdAndCompanyIdInput, Sp_GetNestedCategoryByParentIdAndCompanyIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetNestedCategoryByParentIdAndCompanyIdOutput>> Execute(Sp_GetNestedCategoryByParentIdAndCompanyIdInput request)
		{
			return _dapperExecutor.ExecuteJsonAsync("Sp_GetNestedCategoryByParentIdAndCompanyId", request);
		}


	}


	#endregion



	#region Sp_GetPagedCategories
	[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.4.0 (Newtonsoft.Json v13.0.0.0)")]
	public partial class Sp_GetPagedCategoriesOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int SupCategoryDataId { get; set; }


	}
	[JsonWrapper("@jsonInput")]
	[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.4.0 (Newtonsoft.Json v13.0.0.0)")]
	public partial class Sp_GetPagedCategoriesInput
	{
		[Newtonsoft.Json.JsonProperty("page", Required = Newtonsoft.Json.Required.Always)]
		public int Page { get; set; }

		[Newtonsoft.Json.JsonProperty("rowsPerPage", Required = Newtonsoft.Json.Required.Always)]
		public int RowsPerPage { get; set; }


	}
	public class Sp_GetPagedCategories
	{
		private readonly IDapperExecutor<Sp_GetPagedCategoriesInput, Sp_GetPagedCategoriesOutput> _dapperExecutor;

		public Sp_GetPagedCategories(IDapperExecutor<Sp_GetPagedCategoriesInput, Sp_GetPagedCategoriesOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetPagedCategoriesOutput>> Execute(Sp_GetPagedCategoriesInput request)
		{
			return _dapperExecutor.ExecuteJsonAsync("Sp_GetPagedCategories", request);
		}


	}


	#endregion



	#region Sp_GetProductsByCategoryId
	public class Sp_GetProductsByCategoryIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Title", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Title { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate", Required = Newtonsoft.Json.Required.Default)]
		public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ExpirationDate", Required = Newtonsoft.Json.Required.Default)]
		public System.DateTime ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ProductState", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("Description", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Description { get; set; }

		[Newtonsoft.Json.JsonProperty("Price", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Price { get; set; }

		[Newtonsoft.Json.JsonProperty("KeyWords", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonDataSchema { get; set; }


	}

	public class Sp_GetProductsByCategoryIdInput
	{
		[Newtonsoft.Json.JsonProperty("categoryId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CategoryId { get; set; }


	}

	public class Sp_GetProductsByCategoryId
	{
		private readonly IDapperExecutor<Sp_GetProductsByCategoryIdInput, Sp_GetProductsByCategoryIdOutput> _dapperExecutor;

		public Sp_GetProductsByCategoryId(IDapperExecutor<Sp_GetProductsByCategoryIdInput, Sp_GetProductsByCategoryIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetProductsByCategoryIdOutput>> Execute(Sp_GetProductsByCategoryIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_GetProductsByCategoryId", request);
		}


	}


	#endregion



	#region Sp_CountProductsWithCategoryAndCompany
	public class Sp_CountProductsWithCategoryAndCompanyOutput
	{
		[Newtonsoft.Json.JsonProperty("Result", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 Result { get; set; }


	}

	[JsonWrapper("@jsonInput")]
	[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.4.0 (Newtonsoft.Json v13.0.0.0)")]
	public partial class Sp_CountProductsWithCategoryAndCompanyInput
	{
		[Newtonsoft.Json.JsonProperty("companyId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("categoryId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int CategoryId { get; set; }


	}
	public class Sp_CountProductsWithCategoryAndCompany
	{
		private readonly IDapperExecutor<Sp_CountProductsWithCategoryAndCompanyInput, Sp_CountProductsWithCategoryAndCompanyOutput> _dapperExecutor;

		public Sp_CountProductsWithCategoryAndCompany(IDapperExecutor<Sp_CountProductsWithCategoryAndCompanyInput, Sp_CountProductsWithCategoryAndCompanyOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_CountProductsWithCategoryAndCompanyOutput>> Execute(Sp_CountProductsWithCategoryAndCompanyInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_CountProductsWithCategoryAndCompany", request);
		}


	}


	#endregion



	#region Sp_GetCompanyByName
	public class Sp_GetCompanyByNameOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("SourceId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 SourceId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate", Required = Newtonsoft.Json.Required.Default)]
		public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonDataSchema { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyState", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 CompanyState { get; set; }


	}

	public class Sp_GetCompanyByNameInput
	{
		[Newtonsoft.Json.JsonProperty("companyName", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String CompanyName { get; set; }


	}

	public class Sp_GetCompanyByName
	{
		private readonly IDapperExecutor<Sp_GetCompanyByNameInput, Sp_GetCompanyByNameOutput> _dapperExecutor;

		public Sp_GetCompanyByName(IDapperExecutor<Sp_GetCompanyByNameInput, Sp_GetCompanyByNameOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetCompanyByNameOutput>> Execute(Sp_GetCompanyByNameInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_GetCompanyByName", request);
		}


	}


	#endregion



	#region Sp_GetAllCompanies
	public class Sp_GetAllCompaniesOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("SourceId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 SourceId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate", Required = Newtonsoft.Json.Required.Default)]
		public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonDataSchema { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyState", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 CompanyState { get; set; }


	}


	public class Sp_GetAllCompanies
	{
		private readonly IDapperExecutor<EmptyInputParams, Sp_GetAllCompaniesOutput> _dapperExecutor;

		public Sp_GetAllCompanies(IDapperExecutor<EmptyInputParams, Sp_GetAllCompaniesOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetAllCompaniesOutput>> Execute()
		{
			return _dapperExecutor.ExecuteAsync("Sp_GetAllCompanies");
		}


	}


	#endregion



	#region Sp_GetCompanyById
	public class Sp_GetCompanyByIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("SourceId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 SourceId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate", Required = Newtonsoft.Json.Required.Default)]
		public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonDataSchema { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyState", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 CompanyState { get; set; }


	}

	public class Sp_GetCompanyByIdInput
	{
		[Newtonsoft.Json.JsonProperty("companyId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CompanyId { get; set; }


	}

	public class Sp_GetCompanyById
	{
		private readonly IDapperExecutor<Sp_GetCompanyByIdInput, Sp_GetCompanyByIdOutput> _dapperExecutor;

		public Sp_GetCompanyById(IDapperExecutor<Sp_GetCompanyByIdInput, Sp_GetCompanyByIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetCompanyByIdOutput>> Execute(Sp_GetCompanyByIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_GetCompanyById", request);
		}


	}


	#endregion



	#region Sp_CountProductsWithCompany
	public class Sp_CountProductsWithCompanyOutput
	{
		[Newtonsoft.Json.JsonProperty("Result", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 Result { get; set; }


	}

	public class Sp_CountProductsWithCompanyInput
	{
		[Newtonsoft.Json.JsonProperty("companyId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CompanyId { get; set; }


	}

	public class Sp_CountProductsWithCompany
	{
		private readonly IDapperExecutor<Sp_CountProductsWithCompanyInput, Sp_CountProductsWithCompanyOutput> _dapperExecutor;

		public Sp_CountProductsWithCompany(IDapperExecutor<Sp_CountProductsWithCompanyInput, Sp_CountProductsWithCompanyOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_CountProductsWithCompanyOutput>> Execute(Sp_CountProductsWithCompanyInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_CountProductsWithCompany", request);
		}


	}


	#endregion



	#region Sp_AddCompany

	public class Sp_AddCompanyInput
	{
		[Newtonsoft.Json.JsonProperty("SourceId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 SourceId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonDataSchema { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyState", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CompanyState { get; set; }


	}

	public class Sp_AddCompany
	{
		private readonly IDapperExecutor<Sp_AddCompanyInput> _dapperExecutor;

		public Sp_AddCompany(IDapperExecutor<Sp_AddCompanyInput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task Execute(Sp_AddCompanyInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_AddCompany", request);
		}


	}


	#endregion



	#region Sp_GetAllProductsByCompanyId
	public class Sp_GetAllProductsByCompanyIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Title", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Title { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate", Required = Newtonsoft.Json.Required.Default)]
		public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ExpirationDate", Required = Newtonsoft.Json.Required.Default)]
		public System.DateTime ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ProductState", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("Description", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Description { get; set; }

		[Newtonsoft.Json.JsonProperty("Price", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Price { get; set; }

		[Newtonsoft.Json.JsonProperty("KeyWords", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonDataSchema { get; set; }


	}

	public class Sp_GetAllProductsByCompanyIdInput
	{
		[Newtonsoft.Json.JsonProperty("companyId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CompanyId { get; set; }


	}

	public class Sp_GetAllProductsByCompanyId
	{
		private readonly IDapperExecutor<Sp_GetAllProductsByCompanyIdInput, Sp_GetAllProductsByCompanyIdOutput> _dapperExecutor;

		public Sp_GetAllProductsByCompanyId(IDapperExecutor<Sp_GetAllProductsByCompanyIdInput, Sp_GetAllProductsByCompanyIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetAllProductsByCompanyIdOutput>> Execute(Sp_GetAllProductsByCompanyIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_GetAllProductsByCompanyId", request);
		}


	}


	#endregion



	#region Sp_UpdateProduct

	public class Sp_UpdateProductInput
	{
		[Newtonsoft.Json.JsonProperty("productId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 ProductId { get; set; }

		[Newtonsoft.Json.JsonProperty("companyId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("externalId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("title", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Title { get; set; }

		[Newtonsoft.Json.JsonProperty("url", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("syncDate", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("expirationDate", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.DateTime ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("productState", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("description", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Description { get; set; }

		[Newtonsoft.Json.JsonProperty("price", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Price { get; set; }

		[Newtonsoft.Json.JsonProperty("keyWords", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("jsonData", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("jsonDataSchema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonDataSchema { get; set; }


	}

	public class Sp_UpdateProduct
	{
		private readonly IDapperExecutor<Sp_UpdateProductInput> _dapperExecutor;

		public Sp_UpdateProduct(IDapperExecutor<Sp_UpdateProductInput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task Execute(Sp_UpdateProductInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_UpdateProduct", request);
		}


	}


	#endregion



	#region Sp_UpdateProductState

	public class Sp_UpdateProductStateInput
	{
		[Newtonsoft.Json.JsonProperty("productId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 ProductId { get; set; }

		[Newtonsoft.Json.JsonProperty("productState", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 ProductState { get; set; }


	}

	public class Sp_UpdateProductState
	{
		private readonly IDapperExecutor<Sp_UpdateProductStateInput> _dapperExecutor;

		public Sp_UpdateProductState(IDapperExecutor<Sp_UpdateProductStateInput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task Execute(Sp_UpdateProductStateInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_UpdateProductState", request);
		}


	}


	#endregion



	#region Sp_GetProductById
	public class Sp_GetProductByIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Title", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Title { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate", Required = Newtonsoft.Json.Required.Default)]
		public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ExpirationDate", Required = Newtonsoft.Json.Required.Default)]
		public System.DateTime ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ProductState", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("Description", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Description { get; set; }

		[Newtonsoft.Json.JsonProperty("Price", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Price { get; set; }

		[Newtonsoft.Json.JsonProperty("KeyWords", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonDataSchema { get; set; }


	}

	public class Sp_GetProductByIdInput
	{
		[Newtonsoft.Json.JsonProperty("productId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 ProductId { get; set; }


	}

	public class Sp_GetProductById
	{
		private readonly IDapperExecutor<Sp_GetProductByIdInput, Sp_GetProductByIdOutput> _dapperExecutor;

		public Sp_GetProductById(IDapperExecutor<Sp_GetProductByIdInput, Sp_GetProductByIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetProductByIdOutput>> Execute(Sp_GetProductByIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_GetProductById", request);
		}


	}


	#endregion



	#region Sp_GetCategoriesByProductId
	[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.4.0 (Newtonsoft.Json v13.0.0.0)")]
	public partial class Sp_GetCategoriesByProductIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int SupCategoryDataId { get; set; }


	}
	public class Sp_GetCategoriesByProductIdInput
	{
		[Newtonsoft.Json.JsonProperty("productId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 ProductId { get; set; }


	}

	public class Sp_GetCategoriesByProductId
	{
		private readonly IDapperExecutor<Sp_GetCategoriesByProductIdInput, Sp_GetCategoriesByProductIdOutput> _dapperExecutor;

		public Sp_GetCategoriesByProductId(IDapperExecutor<Sp_GetCategoriesByProductIdInput, Sp_GetCategoriesByProductIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetCategoriesByProductIdOutput>> Execute(Sp_GetCategoriesByProductIdInput request)
		{
			return _dapperExecutor.ExecuteJsonAsync("Sp_GetCategoriesByProductId", request);
		}


	}


	#endregion



	#region Sp_GetCategoryById
	[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.4.0 (Newtonsoft.Json v13.0.0.0)")]
	public partial class Sp_GetCategoryByIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int SupCategoryDataId { get; set; }


	}
	public class Sp_GetCategoryByIdInput
	{
		[Newtonsoft.Json.JsonProperty("categoryId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CategoryId { get; set; }


	}

	public class Sp_GetCategoryById
	{
		private readonly IDapperExecutor<Sp_GetCategoryByIdInput, Sp_GetCategoryByIdOutput> _dapperExecutor;

		public Sp_GetCategoryById(IDapperExecutor<Sp_GetCategoryByIdInput, Sp_GetCategoryByIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetCategoryByIdOutput>> Execute(Sp_GetCategoryByIdInput request)
		{
			return _dapperExecutor.ExecuteJsonAsync("Sp_GetCategoryById", request);
		}


	}


	#endregion



	#region Sp_GetSuccessfulProductsByCompanyId
	public class Sp_GetSuccessfulProductsByCompanyIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Title", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Title { get; set; }

		[Newtonsoft.Json.JsonProperty("Url", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate", Required = Newtonsoft.Json.Required.Default)]
		public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ExpirationDate", Required = Newtonsoft.Json.Required.Default)]
		public System.DateTime ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ProductState", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("Description", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Description { get; set; }

		[Newtonsoft.Json.JsonProperty("Price", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String Price { get; set; }

		[Newtonsoft.Json.JsonProperty("KeyWords", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String JsonDataSchema { get; set; }


	}

	public class Sp_GetSuccessfulProductsByCompanyIdInput
	{
		[Newtonsoft.Json.JsonProperty("companyId", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 CompanyId { get; set; }


	}

	public class Sp_GetSuccessfulProductsByCompanyId
	{
		private readonly IDapperExecutor<Sp_GetSuccessfulProductsByCompanyIdInput, Sp_GetSuccessfulProductsByCompanyIdOutput> _dapperExecutor;

		public Sp_GetSuccessfulProductsByCompanyId(IDapperExecutor<Sp_GetSuccessfulProductsByCompanyIdInput, Sp_GetSuccessfulProductsByCompanyIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetSuccessfulProductsByCompanyIdOutput>> Execute(Sp_GetSuccessfulProductsByCompanyIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_GetSuccessfulProductsByCompanyId", request);
		}


	}


	#endregion



	#region Sp_GetStoredProcedureDefinition
	public class Sp_GetStoredProcedureDefinitionOutput
	{
		[Newtonsoft.Json.JsonProperty("definition", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String definition { get; set; }


	}

	public class Sp_GetStoredProcedureDefinitionInput
	{
		[Newtonsoft.Json.JsonProperty("spName", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String SpName { get; set; }


	}

	public class Sp_GetStoredProcedureDefinition
	{
		private readonly IDapperExecutor<Sp_GetStoredProcedureDefinitionInput, Sp_GetStoredProcedureDefinitionOutput> _dapperExecutor;

		public Sp_GetStoredProcedureDefinition(IDapperExecutor<Sp_GetStoredProcedureDefinitionInput, Sp_GetStoredProcedureDefinitionOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetStoredProcedureDefinitionOutput>> Execute(Sp_GetStoredProcedureDefinitionInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_GetStoredProcedureDefinition", request);
		}


	}


	#endregion



	#region Sp_GetStoredProcedureParameters
	public class Sp_GetStoredProcedureParametersOutput
	{
		[Newtonsoft.Json.JsonProperty("object_id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 object_id { get; set; }

		[Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String name { get; set; }

		[Newtonsoft.Json.JsonProperty("parameter_id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 parameter_id { get; set; }

		[Newtonsoft.Json.JsonProperty("system_type_id", Required = Newtonsoft.Json.Required.Default)]
		public System.Byte system_type_id { get; set; }

		[Newtonsoft.Json.JsonProperty("user_type_id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 user_type_id { get; set; }

		[Newtonsoft.Json.JsonProperty("max_length", Required = Newtonsoft.Json.Required.Default)]
		public System.Int16 max_length { get; set; }

		[Newtonsoft.Json.JsonProperty("precision", Required = Newtonsoft.Json.Required.Default)]
		public System.Byte precision { get; set; }

		[Newtonsoft.Json.JsonProperty("scale", Required = Newtonsoft.Json.Required.Default)]
		public System.Byte scale { get; set; }

		[Newtonsoft.Json.JsonProperty("is_output", Required = Newtonsoft.Json.Required.Default)]
		public System.Boolean is_output { get; set; }

		[Newtonsoft.Json.JsonProperty("is_cursor_ref", Required = Newtonsoft.Json.Required.Default)]
		public System.Boolean is_cursor_ref { get; set; }

		[Newtonsoft.Json.JsonProperty("has_default_value", Required = Newtonsoft.Json.Required.Default)]
		public System.Boolean has_default_value { get; set; }

		[Newtonsoft.Json.JsonProperty("is_xml_document", Required = Newtonsoft.Json.Required.Default)]
		public System.Boolean is_xml_document { get; set; }

		[Newtonsoft.Json.JsonProperty("default_value", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Object default_value { get; set; }

		[Newtonsoft.Json.JsonProperty("xml_collection_id", Required = Newtonsoft.Json.Required.Default)]
		public System.Int32 xml_collection_id { get; set; }

		[Newtonsoft.Json.JsonProperty("is_readonly", Required = Newtonsoft.Json.Required.Default)]
		public System.Boolean is_readonly { get; set; }

		[Newtonsoft.Json.JsonProperty("is_nullable", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean is_nullable { get; set; }

		[Newtonsoft.Json.JsonProperty("encryption_type", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 encryption_type { get; set; }

		[Newtonsoft.Json.JsonProperty("encryption_type_desc", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String encryption_type_desc { get; set; }

		[Newtonsoft.Json.JsonProperty("encryption_algorithm_name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String encryption_algorithm_name { get; set; }

		[Newtonsoft.Json.JsonProperty("column_encryption_key_id", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 column_encryption_key_id { get; set; }

		[Newtonsoft.Json.JsonProperty("column_encryption_key_database_name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String column_encryption_key_database_name { get; set; }


	}

	public class Sp_GetStoredProcedureParametersInput
	{
		[Newtonsoft.Json.JsonProperty("spName", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String SpName { get; set; }


	}

	public class Sp_GetStoredProcedureParameters
	{
		private readonly IDapperExecutor<Sp_GetStoredProcedureParametersInput, Sp_GetStoredProcedureParametersOutput> _dapperExecutor;

		public Sp_GetStoredProcedureParameters(IDapperExecutor<Sp_GetStoredProcedureParametersInput, Sp_GetStoredProcedureParametersOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetStoredProcedureParametersOutput>> Execute(Sp_GetStoredProcedureParametersInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_GetStoredProcedureParameters", request);
		}


	}


	#endregion



	#region Sp_GetStoredProcedureOutputParameters
	public class Sp_GetStoredProcedureOutputParametersOutput
	{
		[Newtonsoft.Json.JsonProperty("Result", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 Sp_GetStoredProcedureOutputParametersResult { get; set; }

		[Newtonsoft.Json.JsonProperty("is_hidden", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean is_hidden { get; set; }

		[Newtonsoft.Json.JsonProperty("column_ordinal", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 column_ordinal { get; set; }

		[Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String name { get; set; }

		[Newtonsoft.Json.JsonProperty("is_nullable", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean is_nullable { get; set; }

		[Newtonsoft.Json.JsonProperty("system_type_id", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 system_type_id { get; set; }

		[Newtonsoft.Json.JsonProperty("system_type_name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String system_type_name { get; set; }

		[Newtonsoft.Json.JsonProperty("max_length", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int16 max_length { get; set; }

		[Newtonsoft.Json.JsonProperty("precision", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Byte precision { get; set; }

		[Newtonsoft.Json.JsonProperty("scale", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Byte scale { get; set; }

		[Newtonsoft.Json.JsonProperty("collation_name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String collation_name { get; set; }

		[Newtonsoft.Json.JsonProperty("user_type_id", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 user_type_id { get; set; }

		[Newtonsoft.Json.JsonProperty("user_type_database", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String user_type_database { get; set; }

		[Newtonsoft.Json.JsonProperty("user_type_schema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String user_type_schema { get; set; }

		[Newtonsoft.Json.JsonProperty("user_type_name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String user_type_name { get; set; }

		[Newtonsoft.Json.JsonProperty("assembly_qualified_type_name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String assembly_qualified_type_name { get; set; }

		[Newtonsoft.Json.JsonProperty("xml_collection_id", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 xml_collection_id { get; set; }

		[Newtonsoft.Json.JsonProperty("xml_collection_database", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String xml_collection_database { get; set; }

		[Newtonsoft.Json.JsonProperty("xml_collection_schema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String xml_collection_schema { get; set; }

		[Newtonsoft.Json.JsonProperty("xml_collection_name", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String xml_collection_name { get; set; }

		[Newtonsoft.Json.JsonProperty("is_xml_document", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean is_xml_document { get; set; }

		[Newtonsoft.Json.JsonProperty("is_case_sensitive", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean is_case_sensitive { get; set; }

		[Newtonsoft.Json.JsonProperty("is_fixed_length_clr_type", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean is_fixed_length_clr_type { get; set; }

		[Newtonsoft.Json.JsonProperty("source_server", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String source_server { get; set; }

		[Newtonsoft.Json.JsonProperty("source_database", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String source_database { get; set; }

		[Newtonsoft.Json.JsonProperty("source_schema", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String source_schema { get; set; }

		[Newtonsoft.Json.JsonProperty("source_table", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String source_table { get; set; }

		[Newtonsoft.Json.JsonProperty("source_column", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String source_column { get; set; }

		[Newtonsoft.Json.JsonProperty("is_identity_column", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean is_identity_column { get; set; }

		[Newtonsoft.Json.JsonProperty("is_part_of_unique_key", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean is_part_of_unique_key { get; set; }

		[Newtonsoft.Json.JsonProperty("is_updateable", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean is_updateable { get; set; }

		[Newtonsoft.Json.JsonProperty("is_computed_column", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean is_computed_column { get; set; }

		[Newtonsoft.Json.JsonProperty("is_sparse_column_set", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean is_sparse_column_set { get; set; }

		[Newtonsoft.Json.JsonProperty("ordinal_in_order_by_list", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int16 ordinal_in_order_by_list { get; set; }

		[Newtonsoft.Json.JsonProperty("order_by_is_descending", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Boolean order_by_is_descending { get; set; }

		[Newtonsoft.Json.JsonProperty("order_by_list_length", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int16 order_by_list_length { get; set; }

		[Newtonsoft.Json.JsonProperty("error_number", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 error_number { get; set; }

		[Newtonsoft.Json.JsonProperty("error_severity", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 error_severity { get; set; }

		[Newtonsoft.Json.JsonProperty("error_state", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 error_state { get; set; }

		[Newtonsoft.Json.JsonProperty("error_message", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String error_message { get; set; }

		[Newtonsoft.Json.JsonProperty("error_type", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.Int32 error_type { get; set; }

		[Newtonsoft.Json.JsonProperty("error_type_desc", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String error_type_desc { get; set; }


	}

	public class Sp_GetStoredProcedureOutputParametersInput
	{
		[Newtonsoft.Json.JsonProperty("spName", Required = Newtonsoft.Json.Required.DisallowNull)]
		public System.String SpName { get; set; }


	}

	public class Sp_GetStoredProcedureOutputParameters
	{
		private readonly IDapperExecutor<Sp_GetStoredProcedureOutputParametersInput, Sp_GetStoredProcedureOutputParametersOutput> _dapperExecutor;

		public Sp_GetStoredProcedureOutputParameters(IDapperExecutor<Sp_GetStoredProcedureOutputParametersInput, Sp_GetStoredProcedureOutputParametersOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}

		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Sp_GetStoredProcedureOutputParametersOutput>> Execute(Sp_GetStoredProcedureOutputParametersInput request)
		{
			return _dapperExecutor.ExecuteAsync("Sp_GetStoredProcedureOutputParameters", request);
		}


	}


	#endregion



	#region Sp_GetStoredProcedureJsonData
	//Model for Sp_GetStoredProcedureJsonData was not found, could not parse this Stored Procedure!
	#endregion



	#region Sp_GetStoredProcedures
	//Model for Sp_GetStoredProcedures was not found, could not parse this Stored Procedure!
	#endregion



}


using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Services.GeneratedClientFile
{

	#region sp_CountProductsWithCategory
	public class sp_CountProductsWithCategoryOutput
	{
		[Newtonsoft.Json.JsonProperty("Result")] public System.Int32 Result { get; set; }

	}

	public class sp_CountProductsWithCategoryInput
	{
		[Newtonsoft.Json.JsonProperty("categoryId")] public System.Int32 CategoryId { get; set; }

	}

	public class sp_CountProductsWithCategory
	{
		private readonly IDapperExecutor<sp_CountProductsWithCategoryInput, sp_CountProductsWithCategoryOutput> _dapperExecutor;
		public sp_CountProductsWithCategory(IDapperExecutor<sp_CountProductsWithCategoryInput, sp_CountProductsWithCategoryOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_CountProductsWithCategoryOutput>> Execute(sp_CountProductsWithCategoryInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_CountProductsWithCategory", request);
		}
	}

	#endregion

	#region sp_GetAllCategories
	public class sp_GetAllCategoriesOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name")] public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId")] public System.Int32 SupCategoryDataId { get; set; }

	}


	public class sp_GetAllCategories
	{
		private readonly IDapperExecutor<EmptyInputParams, sp_GetAllCategoriesOutput> _dapperExecutor;
		public sp_GetAllCategories(IDapperExecutor<EmptyInputParams, sp_GetAllCategoriesOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetAllCategoriesOutput>> Execute()
		{
			return _dapperExecutor.ExecuteAsync("sp_GetAllCategories");
		}
	}

	#endregion

	#region sp_GetAllProducts
	public class sp_GetAllProductsOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyId")] public System.Int32 CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId")] public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Title")] public System.String Title { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ExpirationDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ProductState")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("Description")] public System.String Description { get; set; }

		[Newtonsoft.Json.JsonProperty("Price")] public System.String Price { get; set; }

		[Newtonsoft.Json.JsonProperty("KeyWords")] public System.String KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData")] public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema")] public System.String JsonDataSchema { get; set; }

	}


	public class sp_GetAllProducts
	{
		private readonly IDapperExecutor<EmptyInputParams, sp_GetAllProductsOutput> _dapperExecutor;
		public sp_GetAllProducts(IDapperExecutor<EmptyInputParams, sp_GetAllProductsOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetAllProductsOutput>> Execute()
		{
			return _dapperExecutor.ExecuteAsync("sp_GetAllProducts");
		}
	}

	#endregion

	#region sp_GetNestedCategoryByParentId
	public class sp_GetNestedCategoryByParentIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name")] public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId")] public System.Int32 SupCategoryDataId { get; set; }

	}

	public class sp_GetNestedCategoryByParentIdInput
	{
		[Newtonsoft.Json.JsonProperty("categoryId")] public System.Int32 CategoryId { get; set; }

	}

	public class sp_GetNestedCategoryByParentId
	{
		private readonly IDapperExecutor<sp_GetNestedCategoryByParentIdInput, sp_GetNestedCategoryByParentIdOutput> _dapperExecutor;
		public sp_GetNestedCategoryByParentId(IDapperExecutor<sp_GetNestedCategoryByParentIdInput, sp_GetNestedCategoryByParentIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetNestedCategoryByParentIdOutput>> Execute(sp_GetNestedCategoryByParentIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetNestedCategoryByParentId", request);
		}
	}

	#endregion

	#region sp_GetNestedCategoryByParentIdAndCompanyId
	public class sp_GetNestedCategoryByParentIdAndCompanyIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name")] public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId")] public System.Int32 SupCategoryDataId { get; set; }

	}

	public class sp_GetNestedCategoryByParentIdAndCompanyIdInput
	{
		[Newtonsoft.Json.JsonProperty("categoryId")] public System.Int32 CategoryId { get; set; }

		[Newtonsoft.Json.JsonProperty("companyId")] public System.Int32 CompanyId { get; set; }

	}

	public class sp_GetNestedCategoryByParentIdAndCompanyId
	{
		private readonly IDapperExecutor<sp_GetNestedCategoryByParentIdAndCompanyIdInput, sp_GetNestedCategoryByParentIdAndCompanyIdOutput> _dapperExecutor;
		public sp_GetNestedCategoryByParentIdAndCompanyId(IDapperExecutor<sp_GetNestedCategoryByParentIdAndCompanyIdInput, sp_GetNestedCategoryByParentIdAndCompanyIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetNestedCategoryByParentIdAndCompanyIdOutput>> Execute(sp_GetNestedCategoryByParentIdAndCompanyIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetNestedCategoryByParentIdAndCompanyId", request);
		}
	}

	#endregion

	#region sp_GetPagedCategories
	public class sp_GetPagedCategoriesOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name")] public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId")] public System.Int32 SupCategoryDataId { get; set; }

	}

	public class sp_GetPagedCategoriesInput
	{
		[Newtonsoft.Json.JsonProperty("page")] public System.Int32 Page { get; set; }

		[Newtonsoft.Json.JsonProperty("rowsPerPage")] public System.Int32 RowsPerPage { get; set; }

	}

	public class sp_GetPagedCategories
	{
		private readonly IDapperExecutor<sp_GetPagedCategoriesInput, sp_GetPagedCategoriesOutput> _dapperExecutor;
		public sp_GetPagedCategories(IDapperExecutor<sp_GetPagedCategoriesInput, sp_GetPagedCategoriesOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetPagedCategoriesOutput>> Execute(sp_GetPagedCategoriesInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetPagedCategories", request);
		}
	}

	#endregion

	#region sp_GetProductsByCategoryId
	public class sp_GetProductsByCategoryIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyId")] public System.Int32 CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId")] public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Title")] public System.String Title { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ExpirationDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ProductState")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("Description")] public System.String Description { get; set; }

		[Newtonsoft.Json.JsonProperty("Price")] public System.String Price { get; set; }

		[Newtonsoft.Json.JsonProperty("KeyWords")] public System.String KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData")] public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema")] public System.String JsonDataSchema { get; set; }

	}

	public class sp_GetProductsByCategoryIdInput
	{
		[Newtonsoft.Json.JsonProperty("categoryId")] public System.Int32 CategoryId { get; set; }

	}

	public class sp_GetProductsByCategoryId
	{
		private readonly IDapperExecutor<sp_GetProductsByCategoryIdInput, sp_GetProductsByCategoryIdOutput> _dapperExecutor;
		public sp_GetProductsByCategoryId(IDapperExecutor<sp_GetProductsByCategoryIdInput, sp_GetProductsByCategoryIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetProductsByCategoryIdOutput>> Execute(sp_GetProductsByCategoryIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetProductsByCategoryId", request);
		}
	}

	#endregion

	#region sp_CountProductsWithCategoryAndCompany
	public class sp_CountProductsWithCategoryAndCompanyOutput
	{
		[Newtonsoft.Json.JsonProperty("Result")] public System.Int32 Result { get; set; }

	}

	public class sp_CountProductsWithCategoryAndCompanyInput
	{
		[Newtonsoft.Json.JsonProperty("categoryId")] public System.Int32 CategoryId { get; set; }

		[Newtonsoft.Json.JsonProperty("companyId")] public System.Int32 CompanyId { get; set; }

	}

	public class sp_CountProductsWithCategoryAndCompany
	{
		private readonly IDapperExecutor<sp_CountProductsWithCategoryAndCompanyInput, sp_CountProductsWithCategoryAndCompanyOutput> _dapperExecutor;
		public sp_CountProductsWithCategoryAndCompany(IDapperExecutor<sp_CountProductsWithCategoryAndCompanyInput, sp_CountProductsWithCategoryAndCompanyOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_CountProductsWithCategoryAndCompanyOutput>> Execute(sp_CountProductsWithCategoryAndCompanyInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_CountProductsWithCategoryAndCompany", request);
		}
	}

	#endregion

	#region sp_GetCompanyByName
	public class sp_GetCompanyByNameOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("SourceId")] public System.Int32 SourceId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId")] public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Name")] public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData")] public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema")] public System.String JsonDataSchema { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyState")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 CompanyState { get; set; }

	}

	public class sp_GetCompanyByNameInput
	{
		[Newtonsoft.Json.JsonProperty("companyName")] public System.String CompanyName { get; set; }

	}

	public class sp_GetCompanyByName
	{
		private readonly IDapperExecutor<sp_GetCompanyByNameInput, sp_GetCompanyByNameOutput> _dapperExecutor;
		public sp_GetCompanyByName(IDapperExecutor<sp_GetCompanyByNameInput, sp_GetCompanyByNameOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetCompanyByNameOutput>> Execute(sp_GetCompanyByNameInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetCompanyByName", request);
		}
	}

	#endregion

	#region sp_GetAllCompanies
	public class sp_GetAllCompaniesOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("SourceId")] public System.Int32 SourceId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId")] public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Name")] public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData")] public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema")] public System.String JsonDataSchema { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyState")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 CompanyState { get; set; }

	}


	public class sp_GetAllCompanies
	{
		private readonly IDapperExecutor<EmptyInputParams, sp_GetAllCompaniesOutput> _dapperExecutor;
		public sp_GetAllCompanies(IDapperExecutor<EmptyInputParams, sp_GetAllCompaniesOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetAllCompaniesOutput>> Execute()
		{
			return _dapperExecutor.ExecuteAsync("sp_GetAllCompanies");
		}
	}

	#endregion

	#region sp_GetCompanyById
	public class sp_GetCompanyByIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("SourceId")] public System.Int32 SourceId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId")] public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Name")] public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData")] public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema")] public System.String JsonDataSchema { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyState")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 CompanyState { get; set; }

	}

	public class sp_GetCompanyByIdInput
	{
		[Newtonsoft.Json.JsonProperty("companyId")] public System.Int32 CompanyId { get; set; }

	}

	public class sp_GetCompanyById
	{
		private readonly IDapperExecutor<sp_GetCompanyByIdInput, sp_GetCompanyByIdOutput> _dapperExecutor;
		public sp_GetCompanyById(IDapperExecutor<sp_GetCompanyByIdInput, sp_GetCompanyByIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetCompanyByIdOutput>> Execute(sp_GetCompanyByIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetCompanyById", request);
		}
	}

	#endregion

	#region sp_CountProductsWithCompany
	public class sp_CountProductsWithCompanyOutput
	{
		[Newtonsoft.Json.JsonProperty("Result")] public System.Int32 Result { get; set; }

	}

	public class sp_CountProductsWithCompanyInput
	{
		[Newtonsoft.Json.JsonProperty("companyId")] public System.Int32 CompanyId { get; set; }

	}

	public class sp_CountProductsWithCompany
	{
		private readonly IDapperExecutor<sp_CountProductsWithCompanyInput, sp_CountProductsWithCompanyOutput> _dapperExecutor;
		public sp_CountProductsWithCompany(IDapperExecutor<sp_CountProductsWithCompanyInput, sp_CountProductsWithCompanyOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_CountProductsWithCompanyOutput>> Execute(sp_CountProductsWithCompanyInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_CountProductsWithCompany", request);
		}
	}

	#endregion

	#region sp_AddCompany

	public class sp_AddCompanyInput
	{
		[Newtonsoft.Json.JsonProperty("SourceId")] public System.Int32 SourceId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId")] public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Name")] public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate")] public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData")] public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema")] public System.String JsonDataSchema { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyState")] public System.Int32 CompanyState { get; set; }

	}

	public class sp_AddCompany
	{
		private readonly IDapperExecutor<sp_AddCompanyInput> _dapperExecutor;
		public sp_AddCompany(IDapperExecutor<sp_AddCompanyInput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task Execute(sp_AddCompanyInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_AddCompany", request);
		}
	}

	#endregion

	#region sp_GetAllProductsByCompanyId
	public class sp_GetAllProductsByCompanyIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyId")] public System.Int32 CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId")] public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Title")] public System.String Title { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ExpirationDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ProductState")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("Description")] public System.String Description { get; set; }

		[Newtonsoft.Json.JsonProperty("Price")] public System.String Price { get; set; }

		[Newtonsoft.Json.JsonProperty("KeyWords")] public System.String KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData")] public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema")] public System.String JsonDataSchema { get; set; }

	}

	public class sp_GetAllProductsByCompanyIdInput
	{
		[Newtonsoft.Json.JsonProperty("companyId")] public System.Int32 CompanyId { get; set; }

	}

	public class sp_GetAllProductsByCompanyId
	{
		private readonly IDapperExecutor<sp_GetAllProductsByCompanyIdInput, sp_GetAllProductsByCompanyIdOutput> _dapperExecutor;
		public sp_GetAllProductsByCompanyId(IDapperExecutor<sp_GetAllProductsByCompanyIdInput, sp_GetAllProductsByCompanyIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetAllProductsByCompanyIdOutput>> Execute(sp_GetAllProductsByCompanyIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetAllProductsByCompanyId", request);
		}
	}

	#endregion

	#region sp_UpdateProduct

	public class sp_UpdateProductInput
	{
		[Newtonsoft.Json.JsonProperty("productId")] public System.Int32 ProductId { get; set; }

		[Newtonsoft.Json.JsonProperty("companyId")] public System.Int32 CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("externalId")] public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("title")] public System.String Title { get; set; }

		[Newtonsoft.Json.JsonProperty("url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("syncDate")] public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("expirationDate")] public System.DateTime ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("productState")] public System.Int32 ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("description")] public System.String Description { get; set; }

		[Newtonsoft.Json.JsonProperty("price")] public System.String Price { get; set; }

		[Newtonsoft.Json.JsonProperty("keyWords")] public System.String KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("jsonData")] public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("jsonDataSchema")] public System.String JsonDataSchema { get; set; }

	}

	public class sp_UpdateProduct
	{
		private readonly IDapperExecutor<sp_UpdateProductInput> _dapperExecutor;
		public sp_UpdateProduct(IDapperExecutor<sp_UpdateProductInput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task Execute(sp_UpdateProductInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_UpdateProduct", request);
		}
	}

	#endregion

	#region sp_UpdateProductState

	public class sp_UpdateProductStateInput
	{
		[Newtonsoft.Json.JsonProperty("productId")] public System.Int32 ProductId { get; set; }

		[Newtonsoft.Json.JsonProperty("productState")] public System.Int32 ProductState { get; set; }

	}

	public class sp_UpdateProductState
	{
		private readonly IDapperExecutor<sp_UpdateProductStateInput> _dapperExecutor;
		public sp_UpdateProductState(IDapperExecutor<sp_UpdateProductStateInput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task Execute(sp_UpdateProductStateInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_UpdateProductState", request);
		}
	}

	#endregion

	#region sp_GetProductById
	public class sp_GetProductByIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyId")] public System.Int32 CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId")] public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Title")] public System.String Title { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ExpirationDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ProductState")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("Description")] public System.String Description { get; set; }

		[Newtonsoft.Json.JsonProperty("Price")] public System.String Price { get; set; }

		[Newtonsoft.Json.JsonProperty("KeyWords")] public System.String KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData")] public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema")] public System.String JsonDataSchema { get; set; }

	}

	public class sp_GetProductByIdInput
	{
		[Newtonsoft.Json.JsonProperty("productId")] public System.Int32 ProductId { get; set; }

	}

	public class sp_GetProductById
	{
		private readonly IDapperExecutor<sp_GetProductByIdInput, sp_GetProductByIdOutput> _dapperExecutor;
		public sp_GetProductById(IDapperExecutor<sp_GetProductByIdInput, sp_GetProductByIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetProductByIdOutput>> Execute(sp_GetProductByIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetProductById", request);
		}
	}

	#endregion

	#region sp_GetCategoriesByProductId
	public class sp_GetCategoriesByProductIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name")] public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId")] public System.Int32 SupCategoryDataId { get; set; }

	}

	public class sp_GetCategoriesByProductIdInput
	{
		[Newtonsoft.Json.JsonProperty("productId")] public System.Int32 ProductId { get; set; }

	}

	public class sp_GetCategoriesByProductId
	{
		private readonly IDapperExecutor<sp_GetCategoriesByProductIdInput, sp_GetCategoriesByProductIdOutput> _dapperExecutor;
		public sp_GetCategoriesByProductId(IDapperExecutor<sp_GetCategoriesByProductIdInput, sp_GetCategoriesByProductIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetCategoriesByProductIdOutput>> Execute(sp_GetCategoriesByProductIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetCategoriesByProductId", request);
		}
	}

	#endregion

	#region sp_GetCategoryById
	public class sp_GetCategoryByIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("Name")] public System.String Name { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SupCategoryDataId")] public System.Int32 SupCategoryDataId { get; set; }

	}

	public class sp_GetCategoryByIdInput
	{
		[Newtonsoft.Json.JsonProperty("categoryId")] public System.Int32 CategoryId { get; set; }

	}

	public class sp_GetCategoryById
	{
		private readonly IDapperExecutor<sp_GetCategoryByIdInput, sp_GetCategoryByIdOutput> _dapperExecutor;
		public sp_GetCategoryById(IDapperExecutor<sp_GetCategoryByIdInput, sp_GetCategoryByIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetCategoryByIdOutput>> Execute(sp_GetCategoryByIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetCategoryById", request);
		}
	}

	#endregion

	#region sp_GetSuccessfulProductsByCompanyId
	public class sp_GetSuccessfulProductsByCompanyIdOutput
	{
		[Newtonsoft.Json.JsonProperty("Id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 Id { get; set; }

		[Newtonsoft.Json.JsonProperty("CompanyId")] public System.Int32 CompanyId { get; set; }

		[Newtonsoft.Json.JsonProperty("ExternalId")] public System.String ExternalId { get; set; }

		[Newtonsoft.Json.JsonProperty("Title")] public System.String Title { get; set; }

		[Newtonsoft.Json.JsonProperty("Url")] public System.String Url { get; set; }

		[Newtonsoft.Json.JsonProperty("SyncDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime SyncDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ExpirationDate")] [System.ComponentModel.DataAnnotations.Required()] public System.DateTime ExpirationDate { get; set; }

		[Newtonsoft.Json.JsonProperty("ProductState")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 ProductState { get; set; }

		[Newtonsoft.Json.JsonProperty("Description")] public System.String Description { get; set; }

		[Newtonsoft.Json.JsonProperty("Price")] public System.String Price { get; set; }

		[Newtonsoft.Json.JsonProperty("KeyWords")] public System.String KeyWords { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonData")] public System.String JsonData { get; set; }

		[Newtonsoft.Json.JsonProperty("JsonDataSchema")] public System.String JsonDataSchema { get; set; }

	}

	public class sp_GetSuccessfulProductsByCompanyIdInput
	{
		[Newtonsoft.Json.JsonProperty("companyId")] public System.Int32 CompanyId { get; set; }

	}

	public class sp_GetSuccessfulProductsByCompanyId
	{
		private readonly IDapperExecutor<sp_GetSuccessfulProductsByCompanyIdInput, sp_GetSuccessfulProductsByCompanyIdOutput> _dapperExecutor;
		public sp_GetSuccessfulProductsByCompanyId(IDapperExecutor<sp_GetSuccessfulProductsByCompanyIdInput, sp_GetSuccessfulProductsByCompanyIdOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetSuccessfulProductsByCompanyIdOutput>> Execute(sp_GetSuccessfulProductsByCompanyIdInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetSuccessfulProductsByCompanyId", request);
		}
	}

	#endregion

	#region sp_GetStoredProcedureDefinition
	public class sp_GetStoredProcedureDefinitionOutput
	{
		[Newtonsoft.Json.JsonProperty("object_id")] [System.ComponentModel.DataAnnotations.Required()] public System.Int32 object_id { get; set; }

		[Newtonsoft.Json.JsonProperty("definition")] public System.String definition { get; set; }

	}

	public class sp_GetStoredProcedureDefinitionInput
	{
		[Newtonsoft.Json.JsonProperty("spName")] public System.String SpName { get; set; }

	}

	public class sp_GetStoredProcedureDefinition
	{
		private readonly IDapperExecutor<sp_GetStoredProcedureDefinitionInput, sp_GetStoredProcedureDefinitionOutput> _dapperExecutor;
		public sp_GetStoredProcedureDefinition(IDapperExecutor<sp_GetStoredProcedureDefinitionInput, sp_GetStoredProcedureDefinitionOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetStoredProcedureDefinitionOutput>> Execute(sp_GetStoredProcedureDefinitionInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetStoredProcedureDefinition", request);
		}
	}

	#endregion

	#region sp_GetStoredProcedureParameters
	public class sp_GetStoredProcedureParametersOutput
	{
		[Newtonsoft.Json.JsonProperty("system_type_name")] public System.String system_type_name { get; set; }

		[Newtonsoft.Json.JsonProperty("is_nullable")] public System.Boolean is_nullable { get; set; }

		[Newtonsoft.Json.JsonProperty("name")] public System.String name { get; set; }

	}

	public class sp_GetStoredProcedureParametersInput
	{
		[Newtonsoft.Json.JsonProperty("spName")] public System.String SpName { get; set; }

	}

	public class sp_GetStoredProcedureParameters
	{
		private readonly IDapperExecutor<sp_GetStoredProcedureParametersInput, sp_GetStoredProcedureParametersOutput> _dapperExecutor;
		public sp_GetStoredProcedureParameters(IDapperExecutor<sp_GetStoredProcedureParametersInput, sp_GetStoredProcedureParametersOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetStoredProcedureParametersOutput>> Execute(sp_GetStoredProcedureParametersInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetStoredProcedureParameters", request);
		}
	}

	#endregion

	#region sp_GetStoredProcedureOutputParameters
	public class sp_GetStoredProcedureOutputParametersOutput
	{
		public System.Int32 sp_GetStoredProcedureOutputParametersResult { get; set; }

		[Newtonsoft.Json.JsonProperty("is_hidden")] public System.Boolean is_hidden { get; set; }

		[Newtonsoft.Json.JsonProperty("column_ordinal")] public System.Int32 column_ordinal { get; set; }

		[Newtonsoft.Json.JsonProperty("name")] public System.String name { get; set; }

		[Newtonsoft.Json.JsonProperty("is_nullable")] public System.Boolean is_nullable { get; set; }

		[Newtonsoft.Json.JsonProperty("system_type_id")] public System.Int32 system_type_id { get; set; }

		[Newtonsoft.Json.JsonProperty("system_type_name")] public System.String system_type_name { get; set; }

		[Newtonsoft.Json.JsonProperty("max_length")] public System.Int16 max_length { get; set; }

		[Newtonsoft.Json.JsonProperty("precision")] public System.Byte precision { get; set; }

		[Newtonsoft.Json.JsonProperty("scale")] public System.Byte scale { get; set; }

		[Newtonsoft.Json.JsonProperty("collation_name")] public System.String collation_name { get; set; }

		[Newtonsoft.Json.JsonProperty("user_type_id")] public System.Int32 user_type_id { get; set; }

		[Newtonsoft.Json.JsonProperty("user_type_database")] public System.String user_type_database { get; set; }

		[Newtonsoft.Json.JsonProperty("user_type_schema")] public System.String user_type_schema { get; set; }

		[Newtonsoft.Json.JsonProperty("user_type_name")] public System.String user_type_name { get; set; }

		[Newtonsoft.Json.JsonProperty("assembly_qualified_type_name")] public System.String assembly_qualified_type_name { get; set; }

		[Newtonsoft.Json.JsonProperty("xml_collection_id")] public System.Int32 xml_collection_id { get; set; }

		[Newtonsoft.Json.JsonProperty("xml_collection_database")] public System.String xml_collection_database { get; set; }

		[Newtonsoft.Json.JsonProperty("xml_collection_schema")] public System.String xml_collection_schema { get; set; }

		[Newtonsoft.Json.JsonProperty("xml_collection_name")] public System.String xml_collection_name { get; set; }

		[Newtonsoft.Json.JsonProperty("is_xml_document")] public System.Boolean is_xml_document { get; set; }

		[Newtonsoft.Json.JsonProperty("is_case_sensitive")] public System.Boolean is_case_sensitive { get; set; }

		[Newtonsoft.Json.JsonProperty("is_fixed_length_clr_type")] public System.Boolean is_fixed_length_clr_type { get; set; }

		[Newtonsoft.Json.JsonProperty("source_server")] public System.String source_server { get; set; }

		[Newtonsoft.Json.JsonProperty("source_database")] public System.String source_database { get; set; }

		[Newtonsoft.Json.JsonProperty("source_schema")] public System.String source_schema { get; set; }

		[Newtonsoft.Json.JsonProperty("source_table")] public System.String source_table { get; set; }

		[Newtonsoft.Json.JsonProperty("source_column")] public System.String source_column { get; set; }

		[Newtonsoft.Json.JsonProperty("is_identity_column")] public System.Boolean is_identity_column { get; set; }

		[Newtonsoft.Json.JsonProperty("is_part_of_unique_key")] public System.Boolean is_part_of_unique_key { get; set; }

		[Newtonsoft.Json.JsonProperty("is_updateable")] public System.Boolean is_updateable { get; set; }

		[Newtonsoft.Json.JsonProperty("is_computed_column")] public System.Boolean is_computed_column { get; set; }

		[Newtonsoft.Json.JsonProperty("is_sparse_column_set")] public System.Boolean is_sparse_column_set { get; set; }

		[Newtonsoft.Json.JsonProperty("ordinal_in_order_by_list")] public System.Int16 ordinal_in_order_by_list { get; set; }

		[Newtonsoft.Json.JsonProperty("order_by_is_descending")] public System.Boolean order_by_is_descending { get; set; }

		[Newtonsoft.Json.JsonProperty("order_by_list_length")] public System.Int16 order_by_list_length { get; set; }

		[Newtonsoft.Json.JsonProperty("error_number")] public System.Int32 error_number { get; set; }

		[Newtonsoft.Json.JsonProperty("error_severity")] public System.Int32 error_severity { get; set; }

		[Newtonsoft.Json.JsonProperty("error_state")] public System.Int32 error_state { get; set; }

		[Newtonsoft.Json.JsonProperty("error_message")] public System.String error_message { get; set; }

		[Newtonsoft.Json.JsonProperty("error_type")] public System.Int32 error_type { get; set; }

		[Newtonsoft.Json.JsonProperty("error_type_desc")] public System.String error_type_desc { get; set; }

	}

	public class sp_GetStoredProcedureOutputParametersInput
	{
		[Newtonsoft.Json.JsonProperty("spName")] public System.String SpName { get; set; }

	}

	public class sp_GetStoredProcedureOutputParameters
	{
		private readonly IDapperExecutor<sp_GetStoredProcedureOutputParametersInput, sp_GetStoredProcedureOutputParametersOutput> _dapperExecutor;
		public sp_GetStoredProcedureOutputParameters(IDapperExecutor<sp_GetStoredProcedureOutputParametersInput, sp_GetStoredProcedureOutputParametersOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetStoredProcedureOutputParametersOutput>> Execute(sp_GetStoredProcedureOutputParametersInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetStoredProcedureOutputParameters", request);
		}
	}

	#endregion

	#region sp_GetStoredProcedureJsonData
	public class sp_GetStoredProcedureJsonDataOutput
	{
		[Newtonsoft.Json.JsonProperty("JSON_F52E2B61-18A1-11d1-B105-00805F49916B")] public System.String JSON_F52E2B61_18A1_11d1_B105_00805F49916B { get; set; }

	}

	public class sp_GetStoredProcedureJsonDataInput
	{
		[Newtonsoft.Json.JsonProperty("spName")] public System.String SpName { get; set; }

	}

	public class sp_GetStoredProcedureJsonData
	{
		private readonly IDapperExecutor<sp_GetStoredProcedureJsonDataInput, sp_GetStoredProcedureJsonDataOutput> _dapperExecutor;
		public sp_GetStoredProcedureJsonData(IDapperExecutor<sp_GetStoredProcedureJsonDataInput, sp_GetStoredProcedureJsonDataOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetStoredProcedureJsonDataOutput>> Execute(sp_GetStoredProcedureJsonDataInput request)
		{
			return _dapperExecutor.ExecuteAsync("sp_GetStoredProcedureJsonData", request);
		}
	}

	#endregion

	#region sp_GetStoredProcedures
	public class sp_GetStoredProceduresOutput
	{
		[Newtonsoft.Json.JsonProperty("JSON_F52E2B61-18A1-11d1-B105-00805F49916B")] public System.String JSON_F52E2B61_18A1_11d1_B105_00805F49916B { get; set; }

	}


	public class sp_GetStoredProcedures
	{
		private readonly IDapperExecutor<EmptyInputParams, sp_GetStoredProceduresOutput> _dapperExecutor;
		public sp_GetStoredProcedures(IDapperExecutor<EmptyInputParams, sp_GetStoredProceduresOutput> dapperExecutor)
		{
			this._dapperExecutor = dapperExecutor;
		}
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<sp_GetStoredProceduresOutput>> Execute()
		{
			return _dapperExecutor.ExecuteAsync("sp_GetStoredProcedures");
		}
	}

	#endregion
}
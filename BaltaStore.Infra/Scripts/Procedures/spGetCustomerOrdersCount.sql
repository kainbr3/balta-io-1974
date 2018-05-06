create procedure spGetCustomerOrdersCount
	@Document char(11)
	as
	select	c.[Id],
		CONCAT(c.[FirstName], ' ', c.[LastName]) as Name,
		c.Document,
		count(o.Id) Orders
      from [Customer] c inner join [Order] o on o.CustomerId = c.Id
     where c.Document = @Document
  group by c.Id, CONCAT(c.[FirstName], ' ', c.[LastName]), c.Document
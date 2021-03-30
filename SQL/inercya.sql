SELECT ProductName, CA.CategoryName FROM Products PR
LEFT JOIN Categories CA 
ON PR.CategoryID = CA.CategoryID
WHERE Discontinued = 0
ORDER BY ProductName 

SELECT DISTINCT FirstName, LastName, CU.ContactName  FROM Employees EM
LEFT JOIN Orders ORD 
ON EM.EmployeeID = ORD.EmployeeID
LEFT JOIN Customers CU
ON ORD.CustomerID = CU.CustomerID
WHERE EM.EmployeeID = 1

SELECT 
SUM((OD.UnitPrice * OD.Quantity) - OD.Discount) AS TOTAL_STEVEN_BILLED_BY_YEAR,
YEAR(ORD.OrderDate) AS YEAR_
FROM Orders ORD 
LEFT JOIN [Order Details] OD
ON ORD.OrderID = OD.OrderID
LEFT JOIN Employees EMP
ON EMP.EmployeeID = ORD.EmployeeID
WHERE EMP.FirstName LIKE 'Steven' AND EMP.LastName LIKE 'Buchanan'
GROUP BY YEAR(ORD.OrderDate)
 


WITH subempleados(EmployeeID,FirstName,LastName,ReportsTo) AS
(
 SELECT
	 EmployeeID,
	 FirstName,
	 LastName,
	 ReportsTo
 FROM  Employees
 WHERE ReportsTo IS not NULL

 UNION ALL 

 SELECT
	 EMP.EmployeeID,
	 EMP.FirstName,
	 EMP.LastName,
	 EMP.ReportsTo
 FROM  Employees EMP
 INNER JOIN subempleados as s
  ON EMP.ReportsTo = s.EmployeeID
)

SELECT distinct EmployeeID,FirstName,LastName,ReportsTo FROM subempleados;


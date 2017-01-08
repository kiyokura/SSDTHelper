CREATE PROCEDURE [dbo].[spCalculateMonthlySales]
  @year int,
  @month int
AS
  DECLARE @start DATE = dbo.fncGetFirstDateOfMonth(@year, @month)  
  DECLARE @end DATE = DATEADD(MONTH, 1, @start)

  INSERT INTO MonthlySales
    SELECT d.ShopID, @start AS SalesMonth , SUM(d.Sales)
    FROM DailySales d 
    WHERE d.SalesDate >= @start
      AND d.SalesDate < @end
    GROUP BY d.ShopID

RETURN 0

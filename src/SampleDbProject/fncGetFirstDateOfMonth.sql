CREATE FUNCTION [dbo].[fncGetFirstDateOfMonth]
(
  @year int,
  @month int
)
RETURNS DATE
AS
BEGIN
  RETURN CAST(CAST(@year AS varchar) + '/' + CAST(@month AS varchar) + '/1' AS DATE)
END

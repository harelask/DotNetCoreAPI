Create proc GetCategoryList(
@CategoryId int)
as
begin
select * from Category where CategoryId=@CategoryId
end 
create TRIGGER OnProductInsert ON Products
FOR INSERT
AS begin
	DECLARE @id int;
	DECLARE cursorProduct CURSOR FOR  -- khai báo con trỏ cursorProduct
	SELECT Id FROM inserted     -- dữ liệu trỏ tới


	OPEN cursorProduct                -- Mở con trỏ
	FETCH NEXT FROM cursorProduct     -- Đọc dòng đầu tiên
		INTO @id
	
	WHILE @@FETCH_STATUS = 0          --vòng lặp WHILE khi đọc Cursor thành công
	BEGIN
		insert into ProductActivityTrackings(ProductId, OrderedCount,ReviewCount,SoldCount,ViewCount,RatingAvg)
		values (@id, 0,0,0,0,0)
		FETCH NEXT FROM cursorProduct     -- Đọc dòng đầu tiên
		INTO @id

	END
	CLOSE cursorProduct              -- Đóng Cursor
	DEALLOCATE cursorProduct         -- Giải phóng tài nguyên
end

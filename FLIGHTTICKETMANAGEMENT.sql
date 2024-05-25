CREATE DATABASE FLIGHTTICKETMANAGEMENT

DROP DATABASE FLIGHTTICKETMANAGEMENT

USE FLIGHTTICKETMANAGEMENT

CREATE TABLE CHUYENBAY (
    MaChuyenBay CHAR(10) PRIMARY KEY,
    MaTuyenBay CHAR(10) NOT NULL, 
    MaSanBayDi CHAR(10) NOT NULL, 
    MaSanBayDen CHAR(10) NOT NULL, 
    MaMayBay CHAR(10) NOT NULL, 
    NgayBay DATE NOT NULL,
    GioKhoiHanh TIME NOT NULL,
    ThoiLuong FLOAT NOT NULL,
    DonGia MONEY NOT NULL,
)

CREATE TABLE CTSANBAYTRUNGGIAN (
    MaChuyenBay CHAR(10) NOT NULL,
    MaSanBayTrungGian CHAR(10) NOT NULL,
    ThoiGianDung INT NOT NULL,
    GhiChu NVARCHAR(50),
    PRIMARY KEY (MaChuyenBay, MaSanBayTrungGian)
)

CREATE TABLE SANBAY (
    MaSanBay CHAR(10) PRIMARY KEY,
    TenSanBay NVARCHAR(50) NOT NULL,
    DiaChi NVARCHAR(50) NOT NULL,
    MaQuocGia CHAR(10) NOT NULL
)

CREATE TABLE HANGVE (
    MaHangVe CHAR(10) PRIMARY KEY,
    TenHangVe NVARCHAR(255) NOT NULL,
    TiLeTinhDonGia FLOAT NOT NULL
)

CREATE TABLE CHITIETHANGVE (
    MaHangVe CHAR(10) NOT NULL,
    MaChuyenBay CHAR(10) NOT NULL,
    GiaVe MONEY NOT NULL,
    SoGheChoHangVe INT NOT NULL,
    SoVeDaBan INT NOT NULL,
    SoGheDat INT NOT NULL,
    SoGheConLai AS (SoGheChoHangVe - SoVeDaBan - SoGheDat),
    PRIMARY KEY (MaHangVe, MaChuyenBay)
)

CREATE TABLE HANHKHACH (
	MaHanhKhach CHAR(10) PRIMARY KEY,
	HoTen NVARCHAR(255),
	CCCD VARCHAR(12),
	DienThoai VARCHAR(10),
	Email VARCHAR(255),
)

CREATE TABLE VECHUYENBAY(
	MaVe CHAR(10) PRIMARY KEY,
	NgayXuatVe DATETIME NOT NULL,
	MaChuyenBay CHAR(10) NOT NULL,
	MaHangVe CHAR(10) NOT NULL,
	MaHanhKhach CHAR(10) NOT NULL,
	MaGhe CHAR(10) NOT NULL,
)

CREATE TABLE PHIEUDATCHO(
	SoPhieuDatCho CHAR(10) PRIMARY KEY,
	NgayDat DATETIME,
	MaHanhKhach CHAR(10),
	MaHangVe CHAR(10),
	MaChuyenBay CHAR(10),
	MaGhe CHAR(10),
	TinhTrang NVARCHAR(10),
)

CREATE TABLE CTDOANHTHUTHANG(
	MaChuyenBay CHAR(10),
	Thang INT,
	Nam INT,
	SoVe INT NOT NULL,
	TiLeThang FLOAT NOT NULL,
	DoanhThuThang MONEY NOT NULL,
	PRIMARY KEY (MaChuyenBay, Thang, Nam)
)

CREATE TABLE CTDOANHTHUNAM(
	Thang INT,
	Nam INT,
	SoChuyenBay INT NOT NULL,
	DoanhThuNam MONEY NOT NULL,
	TiLeNam FLOAT NOT NULL,
	PRIMARY KEY (Thang, Nam)
)

CREATE TABLE DOANHTHUNAM(
	NAM INT PRIMARY KEY, 
	TONGDOANHTHUNAM MONEY
)

CREATE TABLE THAMSO(
	SoSanBayTrungGianToiDa tinyint PRIMARY KEY,
	ThoiGianBayToiThieu int NOT NULL,
	ThoiGianDungToiThieu int NOT NULL,
	ThoiGianDungToiDa int NOT NULL,
	ThoiGianDatVeChamNhat tinyint NOT NULL,
	ThoiGianHuyVeDatVe tinyint NOT NULL
)

CREATE TABLE DANHSACHGHECUAMAYBAY(
	MaGhe CHAR(10) PRIMARY KEY, -- VN123A1
	SoGhe CHAR(10) NOT NULL, -- A1
	MaMayBay CHAR(10) NOT NULL, -- VN123
	GhiChu NVARCHAR(20)
)

CREATE TABLE MAYBAY (
	MaMayBay CHAR(10) PRIMARY KEY,
	TenMayBay NVARCHAR(50) NOT NULL,
	MaHangMayBay CHAR(10) NOT NULL,
	SoLuongGhe INT NOT NULL
)

CREATE TABLE HANGMAYBAY(
	MaHangMayBay CHAR(10) PRIMARY KEY,
	TenHang NVARCHAR(50) NOT NULL
)

CREATE TABLE TUYENBAY(
	MaTuyenBay CHAR(10) PRIMARY KEY,
	MaQuocGiaDi CHAR(10) NOT NULL, 
	MaQuocGiaDen CHAR(10) NOT NULL 
)

CREATE TABLE QUOCGIA(
	MaQuocGia CHAR(10) PRIMARY KEY,
	TenQuocGia NVARCHAR(50) NOT NULL
)

CREATE TABLE TAIKHOAN(
	TenTaiKhoan VARCHAR(100) PRIMARY KEY,
	MatKhau VARCHAR(100),
)

ALTER TABLE CHUYENBAY ADD FOREIGN KEY (MaTuyenBay) REFERENCES TUYENBAY(MaTuyenBay)
ALTER TABLE CHUYENBAY ADD FOREIGN KEY (MaSanBayDi) REFERENCES SANBAY(MaSanBay)
ALTER TABLE CHUYENBAY ADD FOREIGN KEY (MaSanBayDen) REFERENCES SANBAY(MaSanBay)  
ALTER TABLE CHUYENBAY ADD FOREIGN KEY (MaMayBay) REFERENCES MAYBAY(MaMayBay)

ALTER TABLE CTSANBAYTRUNGGIAN ADD FOREIGN KEY (MaChuyenBay) REFERENCES CHUYENBAY(MaChuyenBay)

ALTER TABLE SANBAY ADD FOREIGN KEY (MaQuocGia) REFERENCES QUOCGIA(MaQuocGia)

ALTER TABLE CHITIETHANGVE ADD FOREIGN KEY (MaHangVe) REFERENCES HANGVE(MaHangVe)
ALTER TABLE CHITIETHANGVE ADD FOREIGN KEY (MaChuyenBay) REFERENCES CHUYENBAY(MaChuyenBay)

ALTER TABLE VECHUYENBAY ADD FOREIGN KEY (MaHangVe, MaChuyenBay) REFERENCES CHITIETHANGVE(MaHangVe, MaChuyenBay)
ALTER TABLE VECHUYENBAY ADD FOREIGN KEY (MaHanhKhach) REFERENCES HANHKHACH(MaHanhKhach)  
ALTER TABLE VECHUYENBAY ADD FOREIGN KEY (MaGhe) REFERENCES DANHSACHGHECUAMAYBAY(MaGhe)

ALTER TABLE PHIEUDATCHO ADD FOREIGN KEY (MaHangVe, MaChuyenBay) REFERENCES CHITIETHANGVE(MaHangVe, MaChuyenBay)
ALTER TABLE PHIEUDATCHO ADD FOREIGN KEY (MaHanhKhach) REFERENCES HANHKHACH(MaHanhKhach)  
ALTER TABLE PHIEUDATCHO ADD FOREIGN KEY (MaGhe) REFERENCES DANHSACHGHECUAMAYBAY(MaGhe)  

ALTER TABLE CTDOANHTHUTHANG ADD FOREIGN KEY (MaChuyenBay) REFERENCES CHUYENBAY(MaChuyenBay)
ALTER TABLE CTDOANHTHUTHANG ADD FOREIGN KEY (Thang, Nam) REFERENCES CTDOANHTHUNAM(Thang, Nam)

ALTER TABLE CTDOANHTHUNAM ADD FOREIGN KEY (Nam) REFERENCES DOANHTHUNAM(Nam)

ALTER TABLE DANHSACHGHECUAMAYBAY ADD FOREIGN KEY (MaMayBay) REFERENCES MAYBAY(MaMayBay)

ALTER TABLE MAYBAY ADD FOREIGN KEY (MaHangMayBay) REFERENCES HANGMAYBAY(MaHangMayBay)

ALTER TABLE TUYENBAY ADD FOREIGN KEY (MaQuocGiaDi) REFERENCES QUOCGIA(MaQuocGia)
ALTER TABLE TUYENBAY ADD FOREIGN KEY (MaQuocGiaDen) REFERENCES QUOCGIA(MaQuocGia)

GO
CREATE TRIGGER trg_UpdateCTDoanhThuThang
ON CHUYENBAY
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Tạo bảng tạm để chứa kết quả tính toán doanh thu và số vé
    DECLARE @TempTable TABLE (
        MaChuyenBay CHAR(10),
        Thang INT,
        Nam INT,
        SoVe INT,
        DoanhThuThang MONEY
    );

    -- Chèn dữ liệu vào bảng tạm từ các bản ghi được chèn hoặc cập nhật
    INSERT INTO @TempTable (MaChuyenBay, Thang, Nam, SoVe, DoanhThuThang)
    SELECT 
        i.MaChuyenBay,
        MONTH(i.NgayBay) AS Thang,
        YEAR(i.NgayBay) AS Nam,
        0 AS SoVe,
        0 AS DoanhThuThang
    FROM 
        inserted i
    GROUP BY
        i.MaChuyenBay,
        MONTH(i.NgayBay),
        YEAR(i.NgayBay);

    -- Thêm bản ghi mới vào bảng DOANHTHUNAM nếu chưa tồn tại
    INSERT INTO DOANHTHUNAM (NAM, TONGDOANHTHUNAM)
    SELECT DISTINCT
        t.Nam,
        0 AS TONGDOANHTHUNAM -- Giá trị khởi tạo cho DoanhThuNam
    FROM 
        @TempTable t
    WHERE 
        NOT EXISTS (
            SELECT 1 
            FROM DOANHTHUNAM dtn
            WHERE dtn.NAM = t.Nam
        );

    -- Thêm bản ghi mới vào bảng CTDOANHTHUNAM nếu chưa tồn tại
    INSERT INTO CTDOANHTHUNAM (Thang, Nam, SoChuyenBay, DoanhThuNam, TiLeNam)
    SELECT DISTINCT
        t.Thang,
        t.Nam,
        1 AS SoChuyenBay,  -- Số chuyến bay khởi tạo là 1
        t.DoanhThuThang AS DoanhThuNam,  -- Doanh thu tháng cũng chính là doanh thu năm ban đầu
        0 AS TiLeNam  -- Giá trị mặc định cho TiLeNam
    FROM 
        @TempTable t
    WHERE 
        NOT EXISTS (
            SELECT 1 
            FROM CTDOANHTHUNAM ctn
            WHERE ctn.Thang = t.Thang
              AND ctn.Nam = t.Nam
        );
	-- Tính toán số chuyến bay cho các tháng và năm
    DECLARE @UpdateTable TABLE (
        Thang INT,
        Nam INT,
        SoChuyenBay INT
    );

    INSERT INTO @UpdateTable (Thang, Nam, SoChuyenBay)
    SELECT
        t.Thang,
        t.Nam,
        COUNT(t.MaChuyenBay) AS SoChuyenBay
    FROM 
        @TempTable t
    GROUP BY
        t.Thang,
        t.Nam;

    -- Cập nhật các bản ghi hiện có trong CTDOANHTHUNAM
    UPDATE ctn
    SET 
        ctn.SoChuyenBay = ut.SoChuyenBay
    FROM 
        CTDOANHTHUNAM ctn
    JOIN 
        @UpdateTable ut ON ctn.Nam = ut.Nam AND ctn.Thang = ut.Thang;

    -- Thêm bản ghi mới vào CTDOANHTHUTHANG nếu chưa tồn tại
    INSERT INTO CTDOANHTHUTHANG (MaChuyenBay, Thang, Nam, SoVe, TiLeThang, DoanhThuThang)
    SELECT 
        t.MaChuyenBay,
        t.Thang,
        t.Nam,
        t.SoVe,
        0 AS TiLeThang, -- Khởi tạo với 0, cập nhật khi cần
        t.DoanhThuThang
    FROM 
        @TempTable t
    WHERE 
        NOT EXISTS (
            SELECT 1 
            FROM CTDOANHTHUTHANG ctd
            WHERE ctd.MaChuyenBay = t.MaChuyenBay 
              AND ctd.Thang = t.Thang 
              AND ctd.Nam = t.Nam
        );

END;
GO

GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TRIGGER trg_UpdateCTHANGVE 
ON CHITIETHANGVE
FOR INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Tạo bảng tạm để chứa kết quả tính toán doanh thu và số vé
    DECLARE @TempTable TABLE (
        MaChuyenBay CHAR(10),
        Thang INT,
        Nam INT,
        SoVe INT,
        DoanhThuThang MONEY
    );

    -- Chèn dữ liệu vào bảng tạm từ các bản ghi hiện tại trong CHITIETHANGVE
    INSERT INTO @TempTable (MaChuyenBay, Thang, Nam, SoVe, DoanhThuThang)
    SELECT 
        cb.MaChuyenBay,
        MONTH(cb.NgayBay) AS Thang,
        YEAR(cb.NgayBay) AS Nam,
        ISNULL(SUM(cthv.SoVeDaBan), 0) AS SoVe,
        ISNULL(SUM(cthv.GiaVe * cthv.SoVeDaBan), 0) AS DoanhThuThang
    FROM 
        CHITIETHANGVE cthv
    JOIN CHUYENBAY cb ON cthv.MaChuyenBay = cb.MaChuyenBay
    GROUP BY
        cb.MaChuyenBay,
        MONTH(cb.NgayBay),
        YEAR(cb.NgayBay);

    -- Cập nhật vào bảng doanh thu tháng
    UPDATE ctd
    SET 
        ctd.SoVe = t.SoVe,
        ctd.DoanhThuThang = t.DoanhThuThang
    FROM 
        CTDOANHTHUTHANG ctd
    JOIN 
        @TempTable t ON ctd.MaChuyenBay = t.MaChuyenBay
                     AND ctd.Thang = t.Thang
                     AND ctd.Nam = t.Nam;

    -- Tính toán tổng doanh thu năm và lưu vào bảng tạm
    DECLARE @DoanhThuNam TABLE (
        Nam INT,
        DoanhThuThang MONEY
    );

    INSERT INTO @DoanhThuNam (Nam, DoanhThuThang)
    SELECT 
        t.Nam,
        SUM(t.DoanhThuThang) AS DoanhThuThang
    FROM 
        @TempTable t
    GROUP BY
        t.Nam;

    -- Cập nhật tổng doanh thu năm trong bảng DOANHTHUNAM
    UPDATE dtn
    SET 
        dtn.TONGDOANHTHUNAM = dtnTemp.DoanhThuThang
    FROM 
        DOANHTHUNAM dtn
    JOIN 
        @DoanhThuNam dtnTemp ON dtn.NAM = dtnTemp.Nam;

    -- Tính toán số chuyến bay và doanh thu năm cho các tháng và lưu vào bảng tạm
    DECLARE @UpdateCTDoanhThuNam TABLE (
        Thang INT,
        Nam INT,
        SoChuyenBay INT,
        DoanhThuNam MONEY
    );

    INSERT INTO @UpdateCTDoanhThuNam (Thang, Nam, SoChuyenBay, DoanhThuNam)
    SELECT
        t.Thang,
        t.Nam,
        COUNT(t.MaChuyenBay) AS SoChuyenBay,
        SUM(t.DoanhThuThang) AS DoanhThuNam
    FROM 
        @TempTable t
    GROUP BY
        t.Thang,
        t.Nam;

    -- Cập nhật các bản ghi hiện có trong CTDOANHTHUNAM
    UPDATE ctn
    SET 
        ctn.SoChuyenBay = uct.SoChuyenBay,
        ctn.DoanhThuNam = uct.DoanhThuNam
    FROM 
        CTDOANHTHUNAM ctn
    JOIN 
        @UpdateCTDoanhThuNam uct ON ctn.Nam = uct.Nam AND ctn.Thang = uct.Thang;

	DECLARE @DoanhThuThang TABLE (
		Thang int,
		Nam int,
        TongDoanhThuThang MONEY
    );
	INSERT INTO @DoanhThuThang (Thang, Nam, TongDoanhThuThang)
    SELECT 
		t.Thang,
		t.Nam,
        SUM(t.DoanhThuThang) AS DoanhThuThang
    FROM 
        @TempTable t
    GROUP BY
        t.Thang,
		t.Nam;

    -- Cập nhật tỉ lệ doanh thu tháng trong bảng CTDOANHTHUTHANG
    UPDATE ctd
    SET 
        ctd.TiLeThang = ISNULL(ctd.DoanhThuThang / NULLIF(dtt.TongDoanhThuThang, 0) * 100, 0)
    FROM 
        CTDOANHTHUTHANG ctd
    JOIN 
        @DoanhThuThang dtt ON ctd.Thang = dtt.Thang and ctd.Nam = dtt.Nam;

    -- Cập nhật tỉ lệ doanh thu năm trong bảng CTDOANHTHUNAM
    UPDATE ctn
    SET 
        ctn.TiLeNam = ISNULL(ctn.DoanhThuNam / NULLIF(dtn.TONGDOANHTHUNAM, 0) * 100, 0)
    FROM 
        CTDOANHTHUNAM ctn
    JOIN 
        DOANHTHUNAM dtn ON ctn.Nam = dtn.NAM;
END;
GO

Go
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Create the trigger for inserts
CREATE TRIGGER trg_InsertPhieuDatCho
ON PHIEUDATCHO
For INSERT
AS
BEGIN
    -- Tăng SoVeDaBan hoặc SoGheDat dựa trên TinhTrang
    UPDATE CHITIETHANGVE
    SET SoVeDaBan = SoVeDaBan + CASE WHEN i.TinhTrang = N'Đã bán' THEN 1 ELSE 0 END,
        SoGheDat = SoGheDat + CASE WHEN i.TinhTrang = N'Đã đặt' THEN 1 ELSE 0 END
    FROM CHITIETHANGVE chv
    JOIN inserted i ON chv.MaHangVe = i.MaHangVe AND chv.MaChuyenBay = i.MaChuyenBay
END
Go

Go
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Create TRIGGER trg_UpdatePhieuDatCho
ON PHIEUDATCHO
AFTER UPDATE
AS
BEGIN
	declare @Ban NVARCHAR(10);
	declare @Huy NVARCHAR(10);
	select @Ban = u.TinhTrang, @Huy = i.TinhTrang
	from inserted i, deleted u
	if (@Ban = N'Đã bán' and @Huy = N'Đã hủy')
	begin
		print(N'Không thể thay đổi từ Đã bán sang Đã hủy!!')
		rollback transaction
	end

    -- Kiểm tra nếu TinhTrang chuyển từ 'đã đặt' sang 'đã bán' hoặc một số thay đổi khác.
    UPDATE CHITIETHANGVE
    SET SoGheDat = SoGheDat - CASE WHEN u.TinhTrang = N'Đã đặt' THEN 1 ELSE 0 END,
        SoVeDaBan = SoVeDaBan + CASE WHEN i.TinhTrang = N'Đã bán' THEN 1 ELSE 0 END
    FROM CHITIETHANGVE chv
    JOIN inserted i ON chv.MaHangVe = i.MaHangVe AND chv.MaChuyenBay = i.MaChuyenBay
    JOIN deleted u ON u.SoPhieuDatCho = i.SoPhieuDatCho
    WHERE (u.TinhTrang = N'Đã đặt' AND i.TinhTrang <> N'Đã đặt') or (u.TinhTrang = N'Đã hủy' AND i.TinhTrang <> N'Đã hủy')
END
Go

GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TRIGGER trg_AfterInsertVeChuyenBay
ON VECHUYENBAY
AFTER INSERT
AS
BEGIN
    -- Update TinhTrang to 'đã bán' for the corresponding reservation
    UPDATE PHIEUDATCHO
    SET TinhTrang = N'Đã bán'
    FROM PHIEUDATCHO pdc
    JOIN inserted i ON pdc.MaHanhKhach = i.MaHanhKhach
                     AND pdc.MaHangVe = i.MaHangVe
                     AND pdc.MaChuyenBay = i.MaChuyenBay
                     AND pdc.MaGhe = i.MaGhe;
END
GO

GO
CREATE TRIGGER trg_ChuyenBay_ThoiLuong
ON CHUYENBAY
FOR INSERT, UPDATE
AS
BEGIN
    DECLARE @ThoiLuong FLOAT, @ThoiGianBayToiThieu INT
    SELECT @ThoiLuong = ThoiLuong, @ThoiGianBayToiThieu = ThoiGianBayToiThieu
    FROM inserted
    CROSS JOIN THAMSO

    IF @ThoiLuong * 60 < @ThoiGianBayToiThieu
    BEGIN
        RAISERROR('Thời lượng chuyến bay phải lớn hơn hoặc bằng thời gian bay tối thiểu.', 16, 1)
        ROLLBACK TRANSACTION
    END
END
GO

GO
CREATE TRIGGER tr_CTSANBAYTRUNGGIAN_INSERT_UPDATE_Thoigiantoithieu
ON CTSANBAYTRUNGGIAN
FOR INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ThoiGianDungToiThieu INT, @ThoiGianDungToiDa INT;
    SELECT @ThoiGianDungToiThieu = ThoiGianDungToiThieu, @ThoiGianDungToiDa = ThoiGianDungToiDa
    FROM THAMSO;

    IF EXISTS (
        SELECT *
        FROM inserted
        WHERE  @ThoiGianDungToiThieu > ThoiGianDung OR ThoiGianDung > @ThoiGianDungToiDa
    )
    BEGIN
        RAISERROR('Thời gian dừng phải nằm trong khoảng thời gian dừng tối thiểu và tối đa.', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;
GO

GO
CREATE TRIGGER tr_CTSANBAYTRUNGGIAN_INSERT_UPDATE_SoSanbaytrunggian
ON CTSANBAYTRUNGGIAN
FOR INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SoSanBayTrungGianToiDa TINYINT;

    -- Trích xuất số sân bay trung gian tối đa từ bảng THAMSO
    SELECT @SoSanBayTrungGianToiDa = SoSanBayTrungGianToiDa FROM THAMSO;

    -- Kiểm tra số lượng sân bay trung gian của các chuyến bay trong inserted
    IF EXISTS (
        SELECT MaChuyenBay
        FROM CTSANBAYTRUNGGIAN
        WHERE MaChuyenBay IN (SELECT MaChuyenBay FROM inserted)
        GROUP BY MaChuyenBay
        HAVING COUNT(*) > @SoSanBayTrungGianToiDa
    )
    BEGIN
        RAISERROR('Số sân bay trung gian không được vượt quá số sân bay trung gian tối đa.', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;
GO
-------------------------------------------------------------------------------------------------------
go
CREATE TRIGGER TRG_UPDATE_GIAVE
ON CHITIETHANGVE
INSTEAD OF INSERT
AS
BEGIN
    INSERT INTO CHITIETHANGVE (MaHangVe, MaChuyenBay, GiaVe, SoGheChoHangVe, SoVeDaBan, SoGheDat)
    SELECT 
        i.MaHangVe, 
        i.MaChuyenBay, 
        hv.TiLeTinhDonGia * cb.DonGia AS GiaVe, 
        i.SoGheChoHangVe, 
        i.SoVeDaBan, 
        i.SoGheDat
    FROM 
        INSERTED i
        JOIN HANGVE hv ON i.MaHangVe = hv.MaHangVe
        JOIN CHUYENBAY cb ON i.MaChuyenBay = cb.MaChuyenBay;
END;

INSERT INTO QUOCGIA (MaQuocGia, TenQuocGia) VALUES
('VN', N'Việt Nam'),
('SG', N'Singapore'),
('JP', N'Nhật Bản'),
('KR', N'Hàn Quốc'),
('TH', N'Thái Lan');

INSERT INTO SANBAY (MaSanBay, TenSanBay, DiaChi, MaQuocGia) VALUES
('PUS', N'Sân bay Busan', 'Busan', 'KR'),
('ICN', N'Sân bay Seoul', 'Seoul', 'KR'),
('BKK', N'Sân bay Bangkok', 'Bangkok', 'TH'),
('HKT', N'Sân bay Phuket', 'Phuket', 'TH'),
('SINA', N'Sân bay Changi', 'Changi', 'SG'),
('TYOA', N'Sân bay Tokyo', 'Tokyo', 'JP'),
('SGN', N'Sân bay Tân Sơn Nhất', N'Sài Gòn', 'VN'),
('HAN', N'Sân bay quốc tế Nội Bài', N'Hà Nội', 'VN'),
('DAD', N'Sân bay Đà Nẵng', N'Đà Nẵng', 'VN');

INSERT INTO HANGMAYBAY VALUES 
('VA', 'Vietnam Airlines'),
('VJ', 'Viet Jet'),
('BH', 'Bamboo Airway'),
('KAL', 'Korea Air'),
('JL', 'Japan Airlines'),
('TG', 'Thai Airways');

INSERT INTO HANGVE VALUES
('BC', N'Thương Gia', 1.05),
('EC', N'Phổ thông', 1);

INSERT INTO HANHKHACH VALUES
('HK001', N'Nguyễn Khánh Nguyên', '074204432576', '0322339640', 'nguyennk@gmail.com'),
('HK002', N'Nguyễn Thị Hồng', '064204912456', '0982365147', 'hongnt@gmail.com'),
('HK003', N'Trần Trọng Nhân', '078203234567', '0356987123', 'nhantt@gmail.com');

INSERT INTO MAYBAY VALUES
('VA-A886', 'BoeingA886', 'VA', 100),
('VA-A321', 'AirbusA321', 'VA', 150),
('VJ-B103', 'BoeingB103', 'VJ', 200);

INSERT INTO TUYENBAY (MaTuyenBay, MaQuocGiaDi, MaQuocGiaDen) VALUES
('HAN-SGN', 'VN', 'VN'),
('DAD-SGN', 'VN', 'VN'),
('SGN-HAN', 'VN', 'VN'),
('SGN-DAD', 'VN', 'VN'),
('SGN-PUS', 'VN', 'KR'),
('HAN-ICN', 'VN', 'KR'),
('DAD-BKK', 'VN', 'TH'),
('SGN-HKT', 'VN', 'TH'),
('BKK-SGN', 'TH', 'VN'),
('SINA-SGN', 'SG', 'VN'),
('HAN-SINA', 'VN', 'SG'),
('SGN-TYOA', 'VN', 'JP');

INSERT INTO THAMSO VALUES
(2, 30, 10, 20, 24, 0);

INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA886A01', 'A01', 'VA-A886', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA886A02', 'A02', 'VA-A886', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA886A03', 'A03', 'VA-A886', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA886A04', 'A04', 'VA-A886', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA886A05', 'A05', 'VA-A886', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA886A06', 'A06', 'VA-A886', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA886A07', 'A07', 'VA-A886', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA886A08', 'A08', 'VA-A886', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA886A09', 'A09', 'VA-A886', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA886A10', 'A10', 'VA-A886', NULL);

INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B01', 'B01', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B02', 'B02', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B03', 'B03', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B04', 'B04', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B05', 'B05', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B06', 'B06', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B07', 'B07', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B08', 'B08', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B09', 'B09', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B10', 'B10', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B11', 'B11', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B12', 'B12', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B13', 'B13', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B14', 'B14', 'VA-A321', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VAA321B15', 'B15', 'VA-A321', NULL);

INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VJB103C01', 'C01', 'VJ-B103', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VJB103C02', 'C02', 'VJ-B103', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VJB103C03', 'C03', 'VJ-B103', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VJB103C04', 'C04', 'VJ-B103', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VJB103C05', 'C05', 'VJ-B103', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VJB103C06', 'C06', 'VJ-B103', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VJB103C07', 'C07', 'VJ-B103', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VJB103C08', 'C08', 'VJ-B103', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VJB103C09', 'C09', 'VJ-B103', NULL);
INSERT INTO DANHSACHGHECUAMAYBAY VALUES ('VJB103C10', 'C10', 'VJ-B103', NULL);

INSERT INTO TAIKHOAN(TenTaiKhoan, MatKhau) VALUES
('admin1','admin123'),
('user1','user123'),
('user2','user456');


INSERT INTO CHUYENBAY (MaChuyenBay, MaTuyenBay, MaSanBayDi, MaSanBayDen, MaMayBay, NgayBay, GioKhoiHanh, ThoiLuong, DonGia) VALUES
('VA289', 'SGN-DAD', 'SGN', 'DAD', 'VJ-B103', '2023-09-20', '08:00', 1, 1000000),
('VA300', 'SINA-SGN', 'SINA', 'SGN', 'VA-A321', '2023-09-25', '15:00', 2, 1200000),
('VA302', 'SGN-TYOA', 'SGN', 'TYOA', 'VA-A321', '2023-10-12', '10:00', 3, 2000000),
('VA363', 'HAN-SGN', 'HAN', 'SGN', 'VA-A886', '2024-03-16', '08:00', 2, 1500000),
('VA810', 'SGN-HAN', 'SGN', 'HAN', 'VJ-B103', '2024-03-20', '10:00', 2, 1500000),
('VA777', 'SGN-HAN', 'SGN', 'HAN', 'VA-A886', '2024-04-24', '09:00', 2, 1300000),
('VA324', 'HAN-ICN', 'HAN', 'ICN', 'VA-A321', '2024-07-23', '19:00', 4, 869000),
('VA105', 'DAD-BKK', 'DAD', 'BKK', 'VJ-B103', '2024-07-27', '07:00', 1.5, 1100000),
('VA622', 'SGN-HAN', 'SGN', 'HAN', 'VA-A321', '2024-09-12', '14:00', 2, 1600000),
('VJ472', 'DAD-SGN', 'DAD', 'SGN', 'VJ-B103', '2025-01-11', '21:00', 1, 789000);

INSERT INTO CTSANBAYTRUNGGIAN VALUES
('VA 363', 'DAD', 15, NULL),
('VA 105', 'SGN', 17, NULL);

INSERT INTO CHITIETHANGVE (MaHangVe, MaChuyenBay, SoGheChoHangVe, SoVeDaBan, SoGheDat) VALUES
('BC', 'VA289', 3, 1, 0),
('EC', 'VA289', 7, 6, 0),
('BC', 'VA300', 5, 3, 0),
('EC', 'VA300', 10, 5, 0),
('BC', 'VA302', 5, 2, 0),
('EC', 'VA302', 10, 7, 0),
('BC', 'VA363', 3, 0, 0),
('EC', 'VA363', 7, 5, 0),
('BC', 'VA810', 3, 2, 0),
('EC', 'VA810', 7, 4, 0),
('BC', 'VA777', 3, 1, 0),
('EC', 'VA777', 7, 6, 0),
('BC', 'VA324', 5, 0, 0),
('EC', 'VA324', 10, 0, 0),
('BC', 'VA105', 3, 0, 0),
('EC', 'VA105', 7, 0, 0),
('BC', 'VA622', 5, 0, 0),
('EC', 'VA622', 10, 0, 0),
('BC', 'VJ472', 3, 0, 0),
('EC', 'VJ472', 7, 0, 0);



DELETE FROM HANGMAYBAY
DELETE FROM CHUYENBAY
DELETE FROM CHITIETHANGVE

SELECT * FROM DANHSACHGHECUAMAYBAY
SELECT * FROM CTSANBAYTRUNGGIAN
SELECT * FROM CHITIETHANGVE
SELECT * FROM TUYENBAY
SELECT * FROM CHUYENBAY
SELECT * FROM SANBAY
SELECT * FROM MAYBAY
SELECT * FROM HANGVE
SELECT * FROM HANHKHACH
SELECT * FROM HANGMAYBAY
SELECT * FROM SANBAY
SELECT * FROM CTDOANHTHUTHANG
SELECT * FROM THAMSO
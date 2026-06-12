using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LjruiFoodWeb.Data;
using LjruiFoodWeb.Models;

namespace LjruiFoodWeb.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.SanPhams.AnyAsync())
            return;

        var danhMucs = new List<DanhMuc>
        {
            new() { Ma = 1, Ten = "Món Chính", Slug = "mon-chinh", MoTa = "Các món ăn chính hấp dẫn" },
            new() { Ma = 2, Ten = "Khai Vị", Slug = "khai-vi", MoTa = "Món khai vị ngon miệng" },
            new() { Ma = 3, Ten = "Tráng Miệng", Slug = "trang-mieng", MoTa = "Món tráng miệng ngọt ngào" },
            new() { Ma = 4, Ten = "Salad", Slug = "salad", MoTa = "Salad tươi mát" },
            new() { Ma = 5, Ten = "Hải Sản", Slug = "hai-san", MoTa = "Hải sản tươi sống" },
            new() { Ma = 6, Ten = "Đồ Uống", Slug = "do-uong", MoTa = "Đồ uống giải khát" }
        };
        context.DanhMucs.AddRange(danhMucs);
        await context.SaveChangesAsync();

        var sanPhams = new List<SanPham>
        {
            // MÓN CHÍNH
            new()
            {
                Ten = "Baguette Salami",
                Slug = "baguette-salami",
                DanhMucMa = 1,
                HinhAnh = "/images/Baguette-Salami-300x300.jpeg",
                MoTa = "Bánh mì Baguette Pháp giòn tan với lát salami Ý thơm ngon",
                MoTaDayDu = "Baguette Salami là sự kết hợp hoàn hảo giữa bánh mì Baguette truyền thống Pháp với salami Ý đậm đà. Bánh mì được nướng vàng giòn bên ngoài, bên trong mềm xốp, kết hợp với những lát salami mỏng manh, béo ngậy. Đây là món ăn nhanh được yêu thích trên toàn thế giới, phù hợp cho bữa sáng hoặc bữa trưa vội vàng.",
                ThanhPhan = "Bánh mì Baguette, Salami Ý (thịt heo, muối, tiêu, tỏi, men lactic), Phô mai cheddar, Xà lách, Cà chua, Dầu ô liu, Bơ thực vật",
                NguonGoc = "Salami được làm từ thịt heo chọn lọc, ướp gia vị theo công thức truyền thống từ Ý. Bánh Baguette sử dụng bột mì Pháp cao cấp, men tươi và nướng trong lò đốt củi truyền thống.",
                QuyTrinhCheBien = "Bước 1: Nướng bánh Baguette ở 200°C trong 10-12 phút đến khi vàng giòn.\nBước 2: Cắt bánh làm đôi, phết bơ và dầu ô liu.\nBước 3: Xếp lá xà lách, cà chua thái lát.\nBước 4: Đặt 3-4 lát salami lên trên.\nBước 5: Rắc phô mai cheddar bào sợi.\nBước 6: Nướng thêm 2-3 phút cho phô mai tan chảy.",
                KhauPhan = "1 người (1 bánh)",
                BaoQuan = "Nên ăn ngay khi chế biến. Có thể bảo quản trong hộp kín ở nhiệt độ phòng tối đa 2 giờ.",
                GiaHienTai = 55000,
                GiaGoc = 65000,
                PhanTramGiam = 15,
                SoLuongDaBan = 245,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 38,
                SoLuongTon = 50,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Cơm Chiên Hải Sản",
                Slug = "com-chien-hai-san",
                DanhMucMa = 1,
                HinhAnh = "/images/com-chien-hai-san-300x300.png",
                MoTa = "Cơm chiên giòn tan với tôm, mực và cua thơm ngon",
                MoTaDayDu = "Cơm Chiên Hải Sản là món ăn mang đậm hương vị Á Đông, với những hạt cơm được chiên vàng giòn, từng miếng hải sản tươi ngon. Món ăn này không chỉ ngon miệng mà còn cung cấp nguồn protein dồi dào từ hải sản, rất tốt cho sức khỏe.",
                ThanhPhan = "Cơm nguội, Tôm tươi, Mực tươi, Thịt cua, Trứng, Hành tím, Đậu hà lan, Cà rốt, Nước mắm, Dầu ăn, Tiêu",
                NguonGoc = "Tôm và mực được đánh bắt tự nhiên từ vùng biển Việt Nam, đảm bảo độ tươi ngon. Cơm sử dụng gạo ST25 - loại gạo ngon nhất thế giới của Việt Nam.",
                QuyTrinhCheBien = "Bước 1: Nấu cơm từ sáng hôm trước, để nguội hoàn toàn.\nBước 2: Sơ chế hải sản: lột vỏ tôm, cắt mực thành miếng vừa.\nBước 3: Phi hành tím thơm, cho hải sản vào xào sơ qua.\nBước 4: Đánh trứng, cho cơm vào chảo chiên ở lửa lớn.\nBước 5: Thêm hải sản, rau củ, gia vị vào đảo đều.\nBước 6: Chiên đến khi hạt cơm vàng giòn, hải sản chín tới.",
                KhauPhan = "1 người (300g)",
                BaoQuan = "Nên thưởng thức ngay. Có thể hâm nóng trong lò vi sóng hoặc chảo nóng.",
                GiaHienTai = 89000,
                GiaGoc = 99000,
                PhanTramGiam = 10,
                SoLuongDaBan = 189,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 52,
                SoLuongTon = 35,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Cơm Cuộn Hàn Quốc",
                Slug = "com-cuon-han-quoc",
                DanhMucMa = 1,
                HinhAnh = "/images/com-cuon-han-Quoc-300x300.jpeg",
                MoTa = "Món cơm cuộn truyền thống Hàn Quốc với rau củ và thịt",
                MoTaDayDu = "Cơm Cuộn Hàn Quốc (Kimbap) là món ăn đường phố phổ biến nhất tại xứ sở Kim Chi. Những cuộn cơm được cuốn chặt tay với nhân đa dạng bên trong, bên ngoài phủ rong biển giòn tan. Món ăn này không chỉ ngon miệng mà còn rất lành mạnh với sự kết hợp cân bằng giữa tinh bột, protein và rau xanh.",
                ThanhPhan = "Cơm trắng, Rong biển nori, Thịt bò, Trứng, Cà rốt, Dưa leo, Spinach, Đậu phụ, Nước tương, Mè rang",
                NguonGoc = "Cơm cuộn có nguồn gốc từ Hàn Quốc, được cho là đã có từ thế kỷ 19. Nguyên liệu chính được nhập khẩu trực tiếp từ Hàn Quốc, đảm bảo hương vị đích thực.",
                QuyTrinhCheBien = "Bước 1: Nấu cơm, để nguội, trộn với dầu mè và chút muối.\nBước 2: Xào thịt bò với nước tương và tỏi.\nBước 3: Luộc spinach, vắt khô, ướp với xì dầu.\nBước 4: Chiên trứng thành lớp mỏng, cắt sợi.\nBước 5: Đặt tấm tre, trải rong biển, rải cơm đều.\nBước 6: Xếp nhân theo hàng: thịt, trứng, cà rốt, dưa leo, spinach.\nBước 7: Cuộn chặt tay, cắt thành khoanh tròn 2cm.",
                KhauPhan = "1 người (8 khoanh)",
                BaoQuan = "Bảo quản trong hộp kín, có thể để ở nhiệt độ phòng 4-6 giờ. Ăn lạnh hoặc hâm nhẹ đều ngon.",
                GiaHienTai = 75000,
                SoLuongDaBan = 312,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 89,
                SoLuongTon = 40,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Gà Xiên Que Nướng Rau Củ",
                Slug = "ga-xien-que-nuong-rau-cu",
                DanhMucMa = 1,
                HinhAnh = "/images/ga-xien-que-nuong-rau-cu-300x300.jpeg",
                MoTa = "Thịt gà mềm ngọt xiên que nướng với rau củ tươi",
                MoTaDayDu = "Gà Xiên Que Nướng Rau Củ là món ăn mang đậm hương vị nướng than hoa truyền thống. Những miếng gà được tẩm ướp gia vị đậm đà, xiên xen kẽ với các loại rau củ đầy màu sắc, nướng trên bếp than hồng cho đến khi vàng ruộm thơm phức.",
                ThanhPhan = "Thịt ức gà, Nghệ, Tỏi, Sả, Mật ong, Nước tương, Dầu hào, Tiêu, Nấm, Đậu zucchini, Hành tây, Ớt chuông",
                NguonGoc = "Thịt gà được chọn từ gà ta nuôi tự nhiên ở Đồng Nai, thức ăn tự nhiên, không hormone tăng trưởng. Rau củ được trồng theo phương pháp hữu cơ tại Lâm Đồng.",
                QuyTrinhCheBien = "Bước 1: Thịt gà thái miếng vuông 3cm, ướp với nghệ, tỏi, sả, mật ong, nước tương trong 2 giờ.\nBước 2: Cắt rau củ thành miếng vừa với thịt gà.\nBước 3: Xiên xen kẽ: gà - nấm - gà - zucchini - gà - ớt chuông.\nBước 4: Nướng trên bếp than hoa ở lửa vừa, 8-10 phút mỗi mặt.\nBước 5: Lật đều và quét mật ong pha nước tương khi gần chín.\nBước 6: Rắc mè rang và thưởng thức nóng.",
                KhauPhan = "2 người (4 xiên)",
                BaoQuan = "Nên ăn ngay khi nướng xong. Nếu nguội, có thể hâm trong lò 180°C trong 5 phút.",
                GiaHienTai = 125000,
                GiaGoc = 145000,
                PhanTramGiam = 14,
                SoLuongDaBan = 156,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 45,
                SoLuongTon = 25,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Thịt Xông Khói Cuộn Nấm Kim Châm",
                Slug = "thit-xong-khoi-cuon-nam-kim-cham",
                DanhMucMa = 1,
                HinhAnh = "/images/thit-xong-khoi-cuon-nam-kim-cham.jpeg",
                MoTa = "Thịt xông khói giòn cuộn nấm kim châm giòn dai",
                MoTaDayDu = "Một sự kết hợp độc đáo giữa vị mặn của thịt xông khói và vị ngọt tự nhiên của nấm kim châm. Những cuộn thịt được chiên giòn bên ngoài, bên trong là lớp nấm kim châm dai giòn, tạo nên sự hài hòa hoàn hảo.",
                ThanhPhan = "Thịt ba rọi xông khói, Nấm kim châm, Tỏi băm, Tiêu, Dầu ăn, Nước tương, Đường",
                NguonGoc = "Thịt xông khói nhập khẩu từ Mỹ, được hun khói theo công nghệ hiện đại. Nấm kim châm được trồng tại Đà Lạt, vùng khí hậu mát mẻ lý tưởng cho nấm phát triển.",
                QuyTrinhCheBien = "Bước 1: Rửa nấm kim châm, để ráo nước.\nBước 2: Trải miếng thịt xông khói ra, đặt 4-5 sợi nấm kim châm ở đầu.\nBước 3: Cuộn chặt từ đầu có nấm, dùng tăm ghim cố định.\nBước 4: Chiên ngập dầu ở lửa vừa trong 5-7 phút đến khi vàng giòn.\nBước 5: Vớt ra, để ráo dầu, rắc tiêu và thưởng thức.",
                KhauPhan = "2 người (6 cuộn)",
                BaoQuan = "Nên ăn ngay khi chế biến. Có thể bảo quản trong tủ lạnh tối đa 1 ngày.",
                GiaHienTai = 95000,
                SoLuongDaBan = 98,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 28,
                SoLuongTon = 30,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },

            // KHAI VỊ
            new()
            {
                Ten = "Gỏi Cuốn Rau Củ",
                Slug = "goi-cuon-rau-cu",
                DanhMucMa = 2,
                HinhAnh = "/images/goi-cuon-rau-cu-300x300.jpeg",
                MoTa = "Gỏi cuốn tươi mát với rau củ và nước chấm đậm đà",
                MoTaDayDu = "Gỏi Cuốn là món ăn truyền thống của người Việt, đặc biệt phổ biến ở miền Nam. Những chiếc bánh tráng mỏng trong suốt cuốn lấy tôm tươi, thịt heo luộc và rau xanh, ăn kèm với nước mắm chua ngọt hoặc tương đậu phộng.",
                ThanhPhan = "Bánh tráng, Tôm tươi, Thịt heo luộc, Bún tươi, Xà lách, Rau thơm (mint, húng quế), Dưa leo, Cà rốt, Trứng, Đậu phộng rang, Nước mắm, Đường, Chanh, Tỏi, Ớt",
                NguonGoc = "Gỏi cuốn có nguồn gốc từ miền Nam Việt Nam, được cho là xuất hiện từ thế kỷ 19. Nguyên liệu được chọn lọc kỹ càng: tôm đánh bắt tự nhiên từ biển Nam Bộ, rau sống trồng tại vùng ngoại ô TP.HCM.",
                QuyTrinhCheBien = "Bước 1: Luộc thịt heo và tôm, để nguội và thái sợi.\nBước 2: Làm nước chấm: pha nước mắm, đường, chanh, tỏi, ớt.\nBước 3: Trải bánh tráng lên đĩa, xịt nước để mềm.\nBước 4: Đặt xà lách, bún, thịt, tôm, rau thơm, cà rốt.\nBước 5: Cuộn chặt từ dưới lên, gấp hai bên vào trong.\nBước 6: Cắt đôi chéo, ăn với nước mắm và đậu phộng rang.",
                KhauPhan = "2 người (8 cuốn)",
                BaoQuan = "Nên ăn ngay trong vài giờ. Bánh tráng sẽ khô cứng nếu để lâu.",
                GiaHienTai = 65000,
                SoLuongDaBan = 423,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 112,
                SoLuongTon = 45,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Bacon Cuộn Rau Củ",
                Slug = "bacon-cuon-rau-cu",
                DanhMucMa = 2,
                HinhAnh = "/images/bacon-cuon-rau-cu-1-300x300.jpeg",
                MoTa = "Thịt bacon giòn cuộn rau củ xanh tươi",
                MoTaDayDu = "Bacon Cuộn Rau Củ là món khai vị hấp dẫn với những cuộn thịt bacon chiên giòn bên ngoài, bên trong là các loại rau củ tươi ngon. Sự kết hợp giữa vị mặn béo của bacon và vị ngọt tự nhiên của rau củ tạo nên một món ăn cân bằng.",
                ThanhPhan = "Thịt bacon, Măng tây, Cà rốt baby, Bông cải xanh, Nấm đùi gà, Tiêu, Mật ong, Xì dầu",
                NguonGoc = "Bacon được nhập khẩu từ Mỹ, làm từ thịt ba rọi heo ướp muối và xông khói truyền thống. Rau củ được nhập từ các trang trại hữu cơ tại Lâm Đồng.",
                QuyTrinhCheBien = "Bước 1: Rửa và để ráo rau củ, cắt cà rốt và bông cải thành khúc 5cm.\nBước 2: Chần sơ rau củ trong nước sôi 2 phút, ngâm nước đá ngay.\nBước 3: Cuộn bacon quanh từng loại rau củ, cố định bằng tăm.\nBước 4: Chiên trong chảo hoặc nướng lò ở 200°C trong 10-12 phút.\nBước 5: Lật đều đến khi bacon vàng giòn.\nBước 6: Pha sốt mật ong-xì dầu, rưới lên trước khi serve.",
                KhauPhan = "2 người (10 cuốn)",
                BaoQuan = "Nên ăn ngay khi còn nóng và giòn. Có thể bảo quản trong tủ lạnh 1-2 ngày.",
                GiaHienTai = 85000,
                GiaGoc = 95000,
                PhanTramGiam = 11,
                SoLuongDaBan = 187,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 56,
                SoLuongTon = 35,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Pate Sợi",
                Slug = "pate-soi",
                DanhMucMa = 2,
                HinhAnh = "/images/pateso-300x300.jpeg",
                MoTa = "Pate Pháp thơm ngậy ăn kèm bánh mì",
                MoTaDayDu = "Pate Sợi là phiên bản đặc biệt của pate gan truyền thống Pháp, với kết cấu sợi độc đáo mang lại trải nghiệm ăn mới lạ. Pate được làm từ gan heo và thịt heo xay nhuyễn, nêm nếm gia vị cẩn thận, nấu chậm trong nhiều giờ để đạt được hương vị đậm đà.",
                ThanhPhan = "Gan heo, Thịt heo xay, Bơ, Hành tím, Tỏi, Rượu vang trắng, Gia vị (muối, tiêu, nhục thông, húng thyme), Trứng, Kem tươi",
                NguonGoc = "Công thức pate có nguồn gốc từ vùng Alsace, Pháp. Nguyên liệu được chọn lọc kỹ: gan heo tươi từ heo nuôi tự nhiên, rượu vang Pháp cao cấp.",
                QuyTrinhCheBien = "Bước 1: Sơ chế gan heo, ngâm sữa 2 giờ để khử mùi.\nBước 2: Phi hành tím, tỏi thơm, thêm thịt heo xay xào chín.\nBước 3: Cho gan vào nấu chín, thêm rượu vang và gia vị.\nBước 4: Xay nhuyễn hỗn hợp, để lại một ít sợi thịt.\nBước 5: Đổ khuôn, đậy lớp mỡ lợn phủ bề mặt.\nBước 6: Hấp cách thủy trong 1 giờ, để nguội và lạnh qua đêm.",
                KhauPhan = "4 người (200g)",
                BaoQuan = "Bảo quản trong tủ lạnh 5-7 ngày. Có thể đông lạnh để bảo quản lâu hơn.",
                GiaHienTai = 72000,
                SoLuongDaBan = 134,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 42,
                SoLuongTon = 20,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Sandwich Thịt Nguội",
                Slug = "sandwich-thit-nguoi",
                DanhMucMa = 2,
                HinhAnh = "/images/sanwich-thit-nguoi-300x300.jpeg",
                MoTa = "Sandwich thịt nguội Pháp với phô mai và rau xanh",
                MoTaDayDu = "Sandwich Thịt Nguội kiểu Pháp là món ăn nhanh nhưng đầy đủ dinh dưỡng, với lớp thịt nguội thơm ngon, phô mai tan chảy và rau xanh tươi mát kẹp giữa hai lát bánh mì giòn.",
                ThanhPhan = "Bánh mì baguette, Thịt nguội heo, Phô mai emmental, Xà lách, Cà chua, Dưa chuột muối, Bơ, Mustard, Mayonnaise",
                NguonGoc = "Thịt nguội được chế biến từ thịt đùi heo theo công nghệ Châu Âu, ướp muối và hun khói tự nhiên. Phô mai nhập khẩu từ Pháp.",
                QuyTrinhCheBien = "Bước 1: Cắt bánh mì làm đôi theo chiều dài.\nBước 2: Phết một lớp bơ và mustard mỏng.\nBước 3: Xếp thịt nguội gấp đôi.\nBước 4: Thêm phô mai, cà chua, xà lách, dưa chuột muối.\nBước 5: Nhỏ vài giọt mayonnaise.\nBước 6: Đóng nửa bánh, cắt chéo và thưởng thức.",
                KhauPhan = "1 người (1 cái)",
                BaoQuan = "Nên ăn trong vòng 2 giờ sau khi chế biến.",
                GiaHienTai = 48000,
                SoLuongDaBan = 298,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 87,
                SoLuongTon = 50,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Sandwich Cha Tôm",
                Slug = "sandwich-cha-tom",
                DanhMucMa = 2,
                HinhAnh = "/images/sanwich-cha-tom-300x300.jpeg",
                MoTa = "Bánh mì sandwich với chả tôm thơm ngon",
                MoTaDayDu = "Sandwich Chả Tôm là sự sáng tạo độc đáo từ ẩm thực Việt Nam, kết hợp chả tôm chiên giòn với bánh mì mềm xốp. Chả tôm được làm từ tôm tươi xay nhuyễn, nêm nếm vừa miệng, chiên vàng đều.",
                ThanhPhan = "Bánh mì sandwich, Tôm tươi xay, Bột mì, Trứng, Hành tím, Gia vị (muối, tiêu, đường), Rau xà lách, Cà chua, Mayonnaise, Sốt sandwich",
                NguonGoc = "Tôm được đánh bắt tự nhiên từ vùng biển miền Trung Việt Nam, nổi tiếng với tôm sú to và ngọt thịt.",
                QuyTrinhCheBien = "Bước 1: Xay tôm tươi với bột mì, trứng, hành tím và gia vị.\nBước 2: Nặn thành miếng dẹt, chiên vàng ở lửa vừa.\nBước 3: Cắt bánh mì, phết mayonnaise và sốt sandwich.\nBước 4: Đặt chả tôm, xà lách, cà chua.\nBước 5: Đóng bánh, cắt đôi và thưởng thức.",
                KhauPhan = "1 người (1 cái)",
                BaoQuan = "Nên ăn ngay khi chế biến để giữ độ giòn của chả tôm.",
                GiaHienTai = 52000,
                GiaGoc = 60000,
                PhanTramGiam = 13,
                SoLuongDaBan = 176,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 54,
                SoLuongTon = 40,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Hamburger Mini",
                Slug = "hamberger-mini",
                DanhMucMa = 2,
                HinhAnh = "/images/hamberger-mini-1-300x300.jpeg",
                MoTa = "Hamburger nhỏ xinh với bò và phô mai",
                MoTaDayDu = "Hamburger Mini là phiên bản thu nhỏ đáng yêu của hamburger truyền thống, hoàn hảo cho những ai muốn thưởng thức nhiều loại khác nhau hoặc trẻ em.",
                ThanhPhan = "Bánh mì hamburger nhỏ, Thịt bò xay, Phô mai cheddar, Xà lách, Cà chua, Dưa chuột muối, Hành tây, Sốt burger đặc biệt, Mustard, Mayonnaise",
                NguonGoc = "Thịt bò được nhập từ Úc, giống bò Wagyu chất lượng cao, được nuôi bằng cỏ tự nhiên.",
                QuyTrinhCheBien = "Bước 1: Nặn thịt bò thành patty nhỏ 5cm, nêm muối tiêu.\nBước 2: Nướng trên bếp BBQ hoặc chảo nóng ở lửa lớn, 3-4 phút mỗi mặt.\nBước 3: Đặt phô mai lên patty khi gần chín để tan chảy.\nBước 4: Nướng bánh mì vàng nhẹ.\nBước 5: Phết sốt, xếp rau củ và thịt.\nBước 6: Đóng bánh, ghim gập đôi và thưởng thức.",
                KhauPhan = "2 người (4 cái)",
                BaoQuan = "Nên ăn ngay khi chế biến.",
                GiaHienTai = 68000,
                SoLuongDaBan = 234,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 78,
                SoLuongTon = 35,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },

            // HẢI SẢN
            new()
            {
                Ten = "Cá Hồi Áp Chảo Sốt Cay",
                Slug = "ca-hoi-ap-chao-sot-cay",
                DanhMucMa = 5,
                HinhAnh = "/images/ca-hoi-ap-chao-sot-cay-300x300.png",
                MoTa = "Filet cá hồi áp chảo thơm lừng với sốt cay",
                MoTaDayDu = "Cá Hồi Áp Chảo Sốt Cay là món ăn cao cấp kết hợp giữa vị béo ngậy của cá hồi tươi và hương vị cay nồng của sốt đặc biệt. Filet cá hồi được áp chảo ở nhiệt độ cao để giữ lớp da giòn bên ngoài, thịt mềm ngọt bên trong.",
                ThanhPhan = "Filet cá hồi, Ớt fresno, Tỏi, Gừng, Mật ong, Nước tương, Rượu trắng, Bơ, Dầu olive, Tiêu, Rau mầm",
                NguonGoc = "Cá hồi được nhập khẩu từ Na Uy, đánh bắt tự nhiên từ vùng biển lạnh Bắc Đại Tây Dương.",
                QuyTrinhCheBien = "Bước 1: Làm sạch filet cá hồi, thấm khô, ướp muối tiêu.\nBước 2: Làm sốt: phi tỏi gừng, thêm ớt băm, mật ong, nước tương.\nBước 3: Áp chảo cá hồi phần da xuống dưới ở lửa lớn 4 phút.\nBước 4: Lật mặt, thêm bơ và rượu trắng, áp thêm 2 phút.\nBước 5: Rưới sốt cay lên cá, đun thêm 1 phút.\nBước 6: Trang trí với rau mầm và thưởng thức ngay.",
                KhauPhan = "1 người (150g cá)",
                BaoQuan = "Nên ăn ngay khi chế biến. Cá hồi để qua đêm sẽ mất hương vị.",
                GiaHienTai = 185000,
                GiaGoc = 220000,
                PhanTramGiam = 16,
                SoLuongDaBan = 89,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 34,
                SoLuongTon = 15,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Cá Ngừ Cuộn Rau Củ Sốt Nắm Me",
                Slug = "ca-ngu-cuon-rau-cu-sot-nam-me",
                DanhMucMa = 5,
                HinhAnh = "/images/ca-ngu-cuon-rau-cu-sot-nam-me-300x300.jpeg",
                MoTa = "Cá ngừ tươi cuộn rau củ với sốt nắm me chua ngọt",
                MoTaDayDu = "Cá Ngừ Cuộn Rau Củ Sốt Nắm Me là món ăn mang đậm hương vị Đông Á, kết hợp giữa cá ngừ đại dương thơm ngon với các loại rau củ tươi xanh.",
                ThanhPhan = "Cá ngừ đại dương, Rong biển nori, Cà rốt, Dưa leo, Avocado, Xà lách, Bún gạo, Me nguyên vị, Đường, Nước mắm, Ớt, Tỏi",
                NguonGoc = "Cá ngừ được đánh bắt tự nhiên từ Thái Bình Dương. Me được thu hoạch tại miền Tây Việt Nam.",
                QuyTrinhCheBien = "Bước 1: Áp chảo cá ngừ ở lửa lớn, để nguội và thái lát mỏng.\nBước 2: Làm sốt me: nấu me với đường, nước, gia vị đến khi sánh.\nBước 3: Trải rong biển lên tấm tre, đặt bún, rau củ.\nBước 4: Xếp lát cá ngừ lên trên.\nBước 5: Cuộn chặt, cắt khúc 2cm.\nBước 6: Rưới sốt me lên trên và thưởng thức.",
                KhauPhan = "1 người (8 cuốn)",
                BaoQuan = "Nên ăn trong vòng 2 giờ. Bảo quản lạnh nếu cần giữ qua đêm.",
                GiaHienTai = 145000,
                GiaGoc = 165000,
                PhanTramGiam = 12,
                SoLuongDaBan = 67,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 23,
                SoLuongTon = 20,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Canh Gà Sốt Cay",
                Slug = "canh-ga-sot-cay",
                DanhMucMa = 5,
                HinhAnh = "/images/canhgasotcay-300x300.png",
                MoTa = "Canh gà nóng hổi với sốt cay đậm đà",
                MoTaDayDu = "Canh Gà Sốt Cay là món súp mang đậm hương vị Trung Quốc, với những miếng gà mềm ngọt hòa quyện trong nước dùng cay nồng.",
                ThanhPhan = "Thịt gà (đùi, cánh), Nấm đông cô, Tofu, Hành lá, Gừng, Tỏi, Ớt khô, Ớt tươi, Nước dùng gà, Bột năng, Rượu nấu ăn, Gia vị",
                NguonGoc = "Gà được nuôi tại Đồng Nai theo tiêu chuẩn VietGAP. Nấm đông cô nhập khẩu từ Trung Quốc.",
                QuyTrinhCheBien = "Bước 1: Chần gà trong nước sôi, vớt ra rửa sạch.\nBước 2: Phi tỏi gừng thơm, thêm ớt khô và ớt tươi.\nBước 3: Cho gà vào xào sơ, thêm nước dùng.\nBước 4: Nấu sôi 15 phút, thêm nấm đông cô và tofu.\nBước 5: Nêm gia vị, thêm bột năng để sánh.\nBước 6: Rắc hành lá, dầu mè và thưởng thức nóng.",
                KhauPhan = "2 người (500ml)",
                BaoQuan = "Bảo quản trong tủ lạnh 2-3 ngày. Hâm nóng trước khi ăn.",
                GiaHienTai = 95000,
                SoLuongDaBan = 145,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 48,
                SoLuongTon = 25,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Tôm Cocktail",
                Slug = "tom-cocktail",
                DanhMucMa = 5,
                HinhAnh = "/images/tom-cocktail-300x300.jpeg",
                MoTa = "Tôm luộc tươi ngon kiểu cocktail với sốt mayonnaise",
                MoTaDayDu = "Tôm Cocktail là món khai vị thanh lịch, được yêu thích trên toàn thế giới từ những bữa tiệc sang trọng đến các buổi dạ tiệc.",
                ThanhPhan = "Tôm sú to, Trứng, Dầu mayonnaise, Cà chua, Nước cốt chanh, Worcestershire sauce, Tabasco, Đường, Muối, Tiêu, Rau mầm, Chanh",
                NguonGoc = "Tôm sú được nuôi trại tại Bến Tre, vùng nước ngọt lý tưởng cho tôm phát triển to và thịt chắc.",
                QuyTrinhCheBien = "Bước 1: Chần tôm trong nước sôi có muối và chanh trong 3-5 phút.\nBước 2: Vớt ra ngâm nước đá ngay để giữ độ giòn.\nBước 3: Làm sốt cocktail: trộn mayonnaise, cà chua băm, nước cốt chanh, worcestershire, tabasco.\nBước 4: Bóc vỏ tôm, giữ lại đuôi để trang trí.\nBước 5: Xếp tôm trên đĩa với rau mầm.\nBước 6: Đặt sốt cocktail vào ly nhỏ, chấm khi ăn.",
                KhauPhan = "2 người (8 con)",
                BaoQuan = "Nên ăn lạnh. Bảo quản trong tủ lạnh tối đa 1 ngày.",
                GiaHienTai = 135000,
                GiaGoc = 155000,
                PhanTramGiam = 13,
                SoLuongDaBan = 112,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 38,
                SoLuongTon = 20,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Tôm Cuộn Khoai Tây",
                Slug = "tom-cuon-khoai-tay",
                DanhMucMa = 5,
                HinhAnh = "/images/tom-cuon-khoai-tay-300x300.jpeg",
                MoTa = "Tôm tươi cuộn trong lớp khoai tây chiên giòn",
                MoTaDayDu = "Tôm Cuộn Khoai Tây là món ăn sáng tạo kết hợp giữa vị ngọt của tôm tươi và độ giòn của khoai tây chiên.",
                ThanhPhan = "Tôm tươi, Khoai tây, Bột mì, Trứng, Bột chiên giòn, Dầu ăn, Muối, Tiêu, Tỏi bột, Paprika",
                NguonGoc = "Tôm tươi đánh bắt tự nhiên từ vùng biển Nam Việt Nam. Khoai tây nhập từ Mỹ, giống Russet nổi tiếng.",
                QuyTrinhCheBien = "Bước 1: Sơ chế tôm, bóc vỏ giữ đuôi, ướp gia vị.\nBước 2: Khoai tây bào sợi mỏng, vắt ráo nước.\nBước 3: Bọc tôm trong lớp khoai tây, cố định.\nBước 4: Lăn qua bột mì, nhúng trứng, lăn bột chiên giòn.\nBước 5: Chiên ngập dầu ở 175°C trong 3-4 phút đến vàng giòn.\nBước 6: Vớt ra để ráo dầu, rắc paprika và thưởng thức.",
                KhauPhan = "2 người (10 cuốn)",
                BaoQuan = "Nên ăn ngay khi chiên để giữ độ giòn.",
                GiaHienTai = 98000,
                SoLuongDaBan = 89,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 29,
                SoLuongTon = 25,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Tôm Dừa Nướng",
                Slug = "tom-dua-nuong",
                DanhMucMa = 5,
                HinhAnh = "/images/tom-dua-nuong-300x300.png",
                MoTa = "Tôm nướng trong quả dừa với sốt bơ tỏi",
                MoTaDayDu = "Tôm Dừa Nướng là món ăn độc đáo của ẩm thực miền Nam Việt Nam, với những con tôm tươi được nướng nguyên con trong quả dừa.",
                ThanhPhan = "Tôm sú to, Quả dừa tươi, Bơ, Tỏi, Rượu trắng, Nước dừa, Muối, Tiêu, Rau mùi, Chanh",
                NguonGoc = "Dừa được thu hoạch tại Bến Tre - thủ phủ dừa nổi tiếng của Việt Nam. Tôm nhập từ vùng biển Khánh Hòa.",
                QuyTrinhCheBien = "Bước 1: Đục đỉnh dừa, lấy nước và nạo cơm dừa.\nBước 2: Sơ chế tôm, để nguyên vỏ, ướp muối tiêu.\nBước 3: Đặt tôm vào trong quả dừa, thêm nước dừa.\nBước 4: Nướng trên bếp than hoặc lò ở 200°C trong 15 phút.\nBước 5: Làm sốt bơ tỏi: phi bơ với tỏi, thêm rượu trắng.\nBước 6: Rưới sốt bơ tỏi lên tôm, nướng thêm 5 phút.\nBước 7: Trang trí với rau mùi và chanh, ăn ngay.",
                KhauPhan = "2 người (6 con)",
                BaoQuan = "Nên ăn ngay khi nướng xong.",
                GiaHienTai = 165000,
                GiaGoc = 190000,
                PhanTramGiam = 13,
                SoLuongDaBan = 78,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 25,
                SoLuongTon = 15,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Sushi Cá Hồi",
                Slug = "sushi-ca-hoi",
                DanhMucMa = 5,
                HinhAnh = "/images/sushi-ca-hoi-1-300x300.jpeg",
                MoTa = "Sushi cá hồi Nhật Bản tươi ngon",
                MoTaDayDu = "Sushi Cá Hồi là món ăn biểu tượng của ẩm thực Nhật Bản, được yêu thích trên toàn thế giới.",
                ThanhPhan = "Cơm sushi, Cá hồi Na Uy tươi, Rong biển nori, Dầu sesame, Giấm sushi, Đường, Muối, Wasabi, Gừng muối, Nước tương",
                NguonGoc = "Cá hồi được nhập khẩu từ Na Uy - quốc gia hàng đầu về nuôi trồng và xuất khẩu cá hồi Atlantic.",
                QuyTrinhCheBien = "Bước 1: Nấu cơm sushi, trộn với giấm, đường, muối khi còn nóng.\nBước 2: Để cơm nguội, quạt cho nguội nhanh.\nBước 3: Cắt cá hồi thành lát mỏng 0.5cm theo thớ ngang.\nBước 4: Làm ướt tay, nắm cơm thành từng viên nhỏ.\nBước 5: Đặt lát cá hồi lên trên.\nBước 6: Trang trí với dầu sesame, serve với wasabi và gừng.",
                KhauPhan = "2 người (8 miếng)",
                BaoQuan = "Nên ăn ngay trong vòng 30 phút sau khi làm. Cá tươi không bảo quản được lâu.",
                GiaHienTai = 175000,
                GiaGoc = 200000,
                PhanTramGiam = 13,
                SoLuongDaBan = 156,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 67,
                SoLuongTon = 20,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },

            // SALAD
            new()
            {
                Ten = "Salad Caprese",
                Slug = "salad-caprese",
                DanhMucMa = 4,
                HinhAnh = "/images/Salad-Caprese-300x300.jpeg",
                MoTa = "Salad Ý với cà chua, mozzarella và basil",
                MoTaDayDu = "Salad Caprese là món salad mang tính biểu tượng của ẩm thực Ý, được đặt theo tên hòn đảo Capri nơi nó ra đời.",
                ThanhPhan = "Cà chua hữu cơ, Mozzarella fresh, Basil tươi, Dầu olive extra virgin, Giấm balsamic, Muối, Tiêu đen",
                NguonGoc = "Mozzarella được làm tươi từ sữa bò Ý theo phương pháp truyền thống. Cà chua Campari nhập khẩu từ Ý.",
                QuyTrinhCheBien = "Bước 1: Chọn cà chua chín đều, rửa sạch và thái lát tròn.\nBước 2: Cắt mozzarella thành lát mỏng cùng kích thước.\nBước 3: Xếp xen kẽ: cà chua - mozzarella - cà chua.\nBước 4: Đặt lá basil tươi lên trên.\nBước 5: Rưới dầu olive và giấm balsamic.\nBước 6: Rắc muối biển và tiêng đen, serve ngay.",
                KhauPhan = "2 người",
                BaoQuan = "Nên ăn ngay để giữ độ tươi. Mozzarella sẽ chảy nước nếu để lâu.",
                GiaHienTai = 92000,
                SoLuongDaBan = 198,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 72,
                SoLuongTon = 30,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Salad Tôm",
                Slug = "salad-tom",
                DanhMucMa = 4,
                HinhAnh = "/images/salad-tom-1-300x300.jpeg",
                MoTa = "Salad tươi mát với tôm và rau xanh",
                MoTaDayDu = "Salad Tôm là món ăn thanh đạm nhưng đầy đủ dinh dưỡng, hoàn hảo cho những ai đang theo đuổi lối sống healthy.",
                ThanhPhan = "Tôm tươi, Xà lách lolo, Xà lách cos, Rau arugula, Cà chua cherry, Dưa leo, Hành tím, Trứng luộc, Dầu olive, Nước cốt chanh, Mật ong, Muối, Tiêu",
                NguonGoc = "Tôm sú nuôi tại Bến Tre theo tiêu chuẩn organic. Rau xanh trồng tại Lâm Đồng.",
                QuyTrinhCheBien = "Bước 1: Luộc tôm trong nước sôi có muối 3-4 phút.\nBước 2: Ngâm tôm trong nước đá, bóc vỏ giữ đuôi.\nBước 3: Rửa và xếp rau xanh vào đĩa.\nBước 4: Thêm cà chua cherry đôi, dưa leo, trứng cắt đôi.\nBước 5: Xếp tôm lên trên, trang trí với hành tím.\nBước 6: Rưới sốt dầu giấm trước khi ăn.",
                KhauPhan = "1 người",
                BaoQuan = "Nên ăn ngay sau khi thêm sốt để rau không bị héo.",
                GiaHienTai = 115000,
                GiaGoc = 135000,
                PhanTramGiam = 15,
                SoLuongDaBan = 167,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 58,
                SoLuongTon = 25,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },

            // TRÁNG MIỆNG
            new()
            {
                Ten = "Matcha Cheesecake",
                Slug = "matcha-cheesecake",
                DanhMucMa = 3,
                HinhAnh = "/images/Matcha-Chescake-300x300.jpeg",
                MoTa = "Bánh cheese cake trà xanh Nhật Bản",
                MoTaDayDu = "Matcha Cheesecake là sự kết hợp hoàn hảo giữa vị đắng nhẹ của trà xanh Nhật Bản và sự béo ngậy của kem phô mai.",
                ThanhPhan = "Phô mai cream cheese, Trứng, Đường, Kem tươi, Bột matcha ceremonial grade, Bơ, Bánh quy digestive, Gelatin",
                NguonGoc = "Matcha ceremonial grade nhập khẩu từ Uji, Kyoto - vùng trồng trà xanh nổi tiếng nhất Nhật Bản.",
                QuyTrinhCheBien = "Bước 1: Nghiền bánh quy, trộn với bơ tan chảy.\nBước 2: Ép vào đáy khuôn, để lạnh 30 phút.\nBước 3: Đánh phô mai cream cheese với đường đến mịn.\nBước 4: Thêm trứng, kem tươi, gelatin đã ngâm.\nBước 5: Đổ một phần nhân vào khuôn, giữ lại 1/3.\nBước 6: Thêm matcha vào phần nhân còn lại, đổ lên trên.\nBước 7: Nướng cách thủy ở 150°C trong 1 giờ, để nguội trong lò.",
                KhauPhan = "6 người (1 bánh)",
                BaoQuan = "Bảo quản trong tủ lạnh 3-5 ngày. Có thể đông lạnh 1 tháng.",
                GiaHienTai = 155000,
                GiaGoc = 180000,
                PhanTramGiam = 14,
                SoLuongDaBan = 234,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 89,
                SoLuongTon = 15,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Tart Bơ",
                Slug = "tart-bo",
                DanhMucMa = 3,
                HinhAnh = "/images/Tart-Bo-300x300.jpeg",
                MoTa = "Bánh tart Pháp với nhân bơ caramel",
                MoTaDayDu = "Tart Bơ là món bánh ngọt truyền thống của vùng Alsace, Pháp. Lớp vỏ bánh tart giòn rụm bao bọc nhân bơ caramel béo ngậy.",
                ThanhPhan = "Bơ Pháp, Đường, Trứng, Bột mì, Kem tươi, Vanilla, Muối, Sữa",
                NguonGoc = "Bơ Pháp AOP nhập khẩu từ vùng Normandy, Pháp.",
                QuyTrinhCheBien = "Bước 1: Làm vỏ: trộn bột, bơ lạnh cắt khối, trứng, muối.\nBước 2: Bọc màng, để lạnh 1 giờ.\nBước 3: Cán mỏng, đặt vào khuôn tart, đâm lỗ đáy.\nBước 4: Nướng đế vàng ở 180°C trong 15 phút.\nBước 5: Nấu caramel: đường đến vàng mật, thêm bơ cắt khối.\nBước 6: Thêm kem tươi, vanilla, đổ vào đế tart.\nBước 7: Nướng thêm 10 phút, để nguội hoàn toàn.",
                KhauPhan = "8 người (1 bánh)",
                BaoQuan = "Bảo quản trong hộp kín ở nhiệt độ phòng 2-3 ngày.",
                GiaHienTai = 135000,
                SoLuongDaBan = 145,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 52,
                SoLuongTon = 12,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Tiramisu",
                Slug = "tiramisu",
                DanhMucMa = 3,
                HinhAnh = "/images/tiramisu-300x300.jpeg",
                MoTa = "Bánh tiramisu Ý cà phê thơm ngon",
                MoTaDayDu = "Tiramisu là món tráng miệng nổi tiếng nhất của Ý, có nguồn gốc từ vùng Veneto.",
                ThanhPhan = "Phô mai mascarpone, Trứng, Đường, Cà phê espresso, Rượu Marsala, Bánh savoiardi, Bột cacao nguyên chất, Vanilla",
                NguonGoc = "Mascarpone được nhập từ Ý, làm từ kem tươi theo công thức truyền thống.",
                QuyTrinhCheBien = "Bước 1: Pha cà phê espresso, để nguội, thêm rượu Marsala.\nBước 2: Tách trứng, đánh lòng đỏ với đường đến nhạt màu.\nBước 3: Đánh lòng trắng đến bông cứng, trộn nhẹ với mascarpone.\nBước 4: Nhẹ nhàng fold hỗn hợp trứng vào mascarpone.\nBước 5: Nhúng bánh savoiardi vào cà phê (không nhúng lâu).\nBước 6: Xếp một lớp bánh, một lớp kem, xen kẽ.\nBước 7: Phủ bột cacao mịn lên trên, lạnh qua đêm.",
                KhauPhan = "6 người (1 bánh)",
                BaoQuan = "Bảo quản trong tủ lạnh 3-4 ngày. Không đông lạnh.",
                GiaHienTai = 145000,
                GiaGoc = 170000,
                PhanTramGiam = 15,
                SoLuongDaBan = 312,
                SoSaoDanhGia = 5,
                SoLuotDanhGia = 98,
                SoLuongTon = 10,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            },
            new()
            {
                Ten = "Trái Cây",
                Slug = "trai-cay",
                DanhMucMa = 3,
                HinhAnh = "/images/trai-cay-300x300.jpeg",
                MoTa = "Đĩa trái cây tươi theo mùa",
                MoTaDayDu = "Đĩa Trái Cây Tươi là món tráng miệng lành mạnh và tươi mát nhất, với sự kết hợp đa dạng của các loại trái cây theo mùa.",
                ThanhPhan = "Tùy theo mùa: Dâu tây, Việt quất, Xoài, Thanh long, Nho, Cam, Kiwi, Dưa hấu, Đu đủ, Măng cụt (theo mùa)",
                NguonGoc = "Trái cây được nhập từ các vùng trồng uy tín trong nước và nhập khẩu.",
                QuyTrinhCheBien = "Bước 1: Chọn trái cây tươi, rửa sạch với nước muối loãng.\nBước 2: Gọt vỏ và cắt thành miếng vừa ăn.\nBước 3: Sắp xếp đẹp mắt trên đĩa theo màu sắc.\nBước 4: Thêm vài lá bạc hà trang trí.\nBước 5: Serve lạnh với một chút mật ong.",
                KhauPhan = "2 người",
                BaoQuan = "Nên ăn trong ngày. Bảo quản trong tủ lạnh nếu cần giữ qua đêm.",
                GiaHienTai = 75000,
                SoLuongDaBan = 456,
                SoSaoDanhGia = 4,
                SoLuotDanhGia = 134,
                SoLuongTon = 30,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            }
        };

        context.SanPhams.AddRange(sanPhams);
        await context.SaveChangesAsync();
    }

    public static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
    {
        string[] roles = ["Admin", "KhachHang"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(role));
            }
        }
    }

    public static async Task SeedAdminUserAsync(UserManager<NguoiDung> userManager)
    {
        var adminEmail = "admin@ljruifood.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var user = new NguoiDung
            {
                UserName = "admin",
                Email = adminEmail,
                HoTen = "Administrator",
                NgayTao = DateTime.Now
            };
            var result = await userManager.CreateAsync(user, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}

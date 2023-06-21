## PARKING SYSTEM

Check-In
i. Semua kendaraan bebas menggunakan lot yang tersedia
ii. Kendaraan yang boleh masuk hanya Mobil Kecil dan Motor.
iii. Setiap kendaraan yang masuk di catat Berdasarkan Nomor Polisi-nya.
iv. Perhitungan biaya parkir adalah per jam, untuk yang baru masuk sudah di hitung 1 jam.

Check-out
i. Setiap kendaraan yang sudah checkout lot tersebut akan available dan bisa di gunakan oleh orang lain.

Report
i. Dibutuhkan laporan jumlah lot yang terisi.
ii. Dibutuhkan laporan jumlah lot yang tersedia.
iii. Dibutuhkan laporan jumlah kendaraan berdasarkan nomor kendaraan ganjil dan genap.
iv. Dibutuhkan laporan jumlah kendaraan berdasarkan jenis kendaraan.
v. Dibutuhkan laporan jumlah kendaraan berdasarkan warna kendaraan.

Demo Aplikasi :

```bash

dotnet run

===== Parking System Command =====
create_parking_lot {capacity}
park {platNumber | color | type}
leave {slot number}
status
type_of_vehicles {type_vechicle}
registration_numbers_for_vehicles_with_ood_plate
registration_numbers_for_vehicles_with_event_plate
registration_numbers_for_vehicles_with_colour {color_vechicle}
slot_number_for_registration_number {plat_number}
exit

Write your command :create_parking_lot 6
Created a parking lot with 6 slots

slot_numbers_for_vehicles_with_colour Putih

Write your command :park B-1234-XYZ Putih Mobil
Allocated slot number: 1

Write your command :park B-9999-XYZ Putih Motor
Allocated slot number: 2

Write your command :park D-0001-HIJ Hitam Mobil
Allocated slot number: 3

Write your command :park B-7777-DEF Red Mobil
Allocated slot number: 4

Write your command :park B-2701-XXX Biru Mobil
Allocated slot number: 5

Write your command :park B-3141-ZZZ Hitam Motor
Allocated slot number: 6

Write your command :leave 4
Slot number 4 is free

Write your command :status
Slot Plat Number Type Colour
1 B-1234-XYZ Mobil Putih
2 B-9999-XYZ Motor Putih
3 D-0001-HIJ Mobil Hitam
5 B-2701-XXX Mobil Biru
6 B-3141-ZZZ Motor Hitam

Write your command :park B-333-SSS Putih Mobil
Allocated slot number: 4

Write your command :park A-1212-GGG Putih Mobil
Sorry, parking lot is full

Write your command :type_of_vehicles Motor
2

Write your command :type_of_vehicles Mobil
4

Write your command :registration_numbers_for_vehicles_with_ood_plate
B-9999-XYZ,D-0001-HIJ,B-333-SSS,B-2701-XXX,B-3141-ZZZ

Write your command :registration_numbers_for_vehicles_with_event_plate
B-1234-XYZ

Write your command :registration_numbers_for_vehicles_with_colour Putih
B-1234-XYZ,B-9999-XYZ

Write your command :registration_numbers_for_vehicles_with_colour Hitam
Not found.

Write your command :slot_numbers_for_vehicles_with_colour Putih
1, 2

Write your command :slot_number_for_registration_number B-1234-XYZ
1

Write your command :exit

```

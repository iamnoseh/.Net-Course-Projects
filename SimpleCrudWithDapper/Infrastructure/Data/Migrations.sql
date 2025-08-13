create database productdb;
create table products
(
    id serial primary key ,
    name varchar(80),
    price decimal(10,3),
    createdat date
);


INSERT INTO products (name, price, createdat) VALUES
                                                  ('Laptop HP Pavilion 15', 950.250, '2025-01-15'),
                                                  ('Smartphone Samsung Galaxy S24', 1200.000, '2025-02-01'),
                                                  ('Tablet Apple iPad Air', 850.500, '2025-01-20'),
                                                  ('Headphones Sony WH-1000XM5', 350.000, '2025-02-10'),
                                                  ('Smartwatch Apple Watch Series 9', 499.990, '2025-01-25'),
                                                  ('Gaming Mouse Logitech G502', 79.990, '2025-02-14'),
                                                  ('Mechanical Keyboard Razer BlackWidow', 139.750, '2025-01-30'),
                                                  ('4K Monitor Dell UltraSharp', 699.000, '2025-02-18'),
                                                  ('External SSD Samsung T7 1TB', 129.990, '2025-02-05'),
                                                  ('Bluetooth Speaker JBL Charge 5', 179.990, '2025-01-19'),
                                                  ('Router TP-Link Archer AX50', 89.990, '2025-02-08'),
                                                  ('Power Bank Anker 20000mAh', 59.990, '2025-01-28'),
                                                  ('USB Flash Drive SanDisk 128GB', 25.500, '2025-02-11'),
                                                  ('Printer HP LaserJet Pro', 199.990, '2025-01-21'),
                                                  ('Webcam Logitech C920', 99.990, '2025-02-03'),
                                                  ('Microphone Blue Yeti', 129.000, '2025-01-17'),
                                                  ('Drone DJI Mini 3 Pro', 799.000, '2025-02-12'),
                                                  ('Action Camera GoPro HERO11', 399.500, '2025-01-27'),
                                                  ('Electric Scooter Xiaomi Pro 2', 650.000, '2025-02-07'),
                                                  ('Smart TV LG OLED55', 1299.990, '2025-01-29'),
                                                  ('VR Headset Meta Quest 3', 599.990, '2025-02-09'),
                                                  ('Digital Camera Canon EOS M50', 750.000, '2025-01-23'),
                                                  ('Tripod Manfrotto Compact', 89.500, '2025-02-04'),
                                                  ('Smart Home Hub Amazon Echo', 99.000, '2025-01-18'),
                                                  ('Air Purifier Philips Series 3000i', 399.000, '2025-02-13'),
                                                  ('Coffee Maker DeLonghi Magnifica', 499.000, '2025-01-26'),
                                                  ('Gaming Chair Secretlab Titan', 449.000, '2025-02-06'),
                                                  ('Electric Kettle Xiaomi Mi', 45.990, '2025-01-22'),
                                                  ('Washing Machine Samsung EcoBubble', 799.990, '2025-02-02'),
                                                  ('Refrigerator LG InstaView', 1499.990, '2025-01-31');

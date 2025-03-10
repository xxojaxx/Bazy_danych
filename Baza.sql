PGDMP      -    
    	        }            ERP DB    17.4    17.0 u    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    16388    ERP DB    DATABASE     n   CREATE DATABASE "ERP DB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'pl-PL';
    DROP DATABASE "ERP DB";
                     postgres    false            �            1259    16413    Address    TABLE     �   CREATE TABLE public."Address" (
    "AddressId" integer NOT NULL,
    "Country" character varying(255) NOT NULL,
    "PostalCode" character varying(20) NOT NULL,
    "City" character varying(255) NOT NULL,
    "Street" character varying(255) NOT NULL
);
    DROP TABLE public."Address";
       public         heap r       postgres    false            �            1259    16412    Address_AddressId_seq    SEQUENCE     �   CREATE SEQUENCE public."Address_AddressId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public."Address_AddressId_seq";
       public               postgres    false    222            �           0    0    Address_AddressId_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public."Address_AddressId_seq" OWNED BY public."Address"."AddressId";
          public               postgres    false    221            �            1259    16564    Bom    TABLE     h   CREATE TABLE public."Bom" (
    "BomId" integer NOT NULL,
    "Name" character varying(255) NOT NULL
);
    DROP TABLE public."Bom";
       public         heap r       postgres    false            �            1259    16570    BomItem    TABLE     �   CREATE TABLE public."BomItem" (
    "BomId" integer NOT NULL,
    "ItemId" integer NOT NULL,
    "Quantity" numeric(18,6) NOT NULL
);
    DROP TABLE public."BomItem";
       public         heap r       postgres    false            �            1259    16563    Bom_BomId_seq    SEQUENCE     �   CREATE SEQUENCE public."Bom_BomId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public."Bom_BomId_seq";
       public               postgres    false    243            �           0    0    Bom_BomId_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public."Bom_BomId_seq" OWNED BY public."Bom"."BomId";
          public               postgres    false    242            �            1259    16422    Client    TABLE     �   CREATE TABLE public."Client" (
    "ClientId" integer NOT NULL,
    "Name" character varying(255) NOT NULL,
    "AddressId" integer NOT NULL
);
    DROP TABLE public."Client";
       public         heap r       postgres    false            �            1259    16421    Client_ClientId_seq    SEQUENCE     �   CREATE SEQUENCE public."Client_ClientId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE public."Client_ClientId_seq";
       public               postgres    false    224            �           0    0    Client_ClientId_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public."Client_ClientId_seq" OWNED BY public."Client"."ClientId";
          public               postgres    false    223            �            1259    16445    Currency    TABLE     �   CREATE TABLE public."Currency" (
    "CurrencyId" character varying(10) NOT NULL,
    "Name" character varying(255) NOT NULL,
    "Symbol" character varying(10) NOT NULL,
    "ExchangeRate" numeric(18,6) NOT NULL
);
    DROP TABLE public."Currency";
       public         heap r       postgres    false            �            1259    16484 	   Inventory    TABLE     �   CREATE TABLE public."Inventory" (
    "InventoryId" integer NOT NULL,
    "ItemId" integer NOT NULL,
    "Quantity" numeric(18,6) NOT NULL
);
    DROP TABLE public."Inventory";
       public         heap r       postgres    false            �            1259    16483    Inventory_InventoryId_seq    SEQUENCE     �   CREATE SEQUENCE public."Inventory_InventoryId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE public."Inventory_InventoryId_seq";
       public               postgres    false    235            �           0    0    Inventory_InventoryId_seq    SEQUENCE OWNED BY     ]   ALTER SEQUENCE public."Inventory_InventoryId_seq" OWNED BY public."Inventory"."InventoryId";
          public               postgres    false    234            �            1259    16465    Item    TABLE     �   CREATE TABLE public."Item" (
    "ItemId" integer NOT NULL,
    "Name" character varying(255) NOT NULL,
    "Description" text,
    "UmId" integer NOT NULL,
    "ItemType" integer NOT NULL
);
    DROP TABLE public."Item";
       public         heap r       postgres    false            �            1259    16464    Item_ItemId_seq    SEQUENCE     �   CREATE SEQUENCE public."Item_ItemId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public."Item_ItemId_seq";
       public               postgres    false    233            �           0    0    Item_ItemId_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public."Item_ItemId_seq" OWNED BY public."Item"."ItemId";
          public               postgres    false    232            �            1259    16496    Order    TABLE     �   CREATE TABLE public."Order" (
    "OrderId" integer NOT NULL,
    "ClientId" integer NOT NULL,
    "Date" timestamp without time zone DEFAULT now() NOT NULL,
    "Status" character varying(50) NOT NULL,
    "CurrencyId" character varying(10) NOT NULL
);
    DROP TABLE public."Order";
       public         heap r       postgres    false            �            1259    16513 	   OrderItem    TABLE     �   CREATE TABLE public."OrderItem" (
    "OrderId" integer NOT NULL,
    "ItemId" integer NOT NULL,
    "Quantity" numeric(18,6) NOT NULL,
    "Price" numeric(18,6) NOT NULL,
    "DispatchedQuantity" numeric(18,6) DEFAULT 0 NOT NULL
);
    DROP TABLE public."OrderItem";
       public         heap r       postgres    false            �            1259    16495    Order_OrderId_seq    SEQUENCE     �   CREATE SEQUENCE public."Order_OrderId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public."Order_OrderId_seq";
       public               postgres    false    237            �           0    0    Order_OrderId_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public."Order_OrderId_seq" OWNED BY public."Order"."OrderId";
          public               postgres    false    236            �            1259    16390    Role    TABLE     j   CREATE TABLE public."Role" (
    "RoleId" integer NOT NULL,
    "Name" character varying(255) NOT NULL
);
    DROP TABLE public."Role";
       public         heap r       postgres    false            �            1259    16389    Role_RoleId_seq    SEQUENCE     �   CREATE SEQUENCE public."Role_RoleId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public."Role_RoleId_seq";
       public               postgres    false    218            �           0    0    Role_RoleId_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public."Role_RoleId_seq" OWNED BY public."Role"."RoleId";
          public               postgres    false    217            �            1259    16434    Supplier    TABLE     �   CREATE TABLE public."Supplier" (
    "SupplierId" integer NOT NULL,
    "Name" character varying(255) NOT NULL,
    "AddressId" integer NOT NULL
);
    DROP TABLE public."Supplier";
       public         heap r       postgres    false            �            1259    16433    Supplier_SupplierId_seq    SEQUENCE     �   CREATE SEQUENCE public."Supplier_SupplierId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public."Supplier_SupplierId_seq";
       public               postgres    false    226            �           0    0    Supplier_SupplierId_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE public."Supplier_SupplierId_seq" OWNED BY public."Supplier"."SupplierId";
          public               postgres    false    225            �            1259    16530    Supply    TABLE       CREATE TABLE public."Supply" (
    "SupplyId" integer NOT NULL,
    "SupplierId" integer NOT NULL,
    "Date" timestamp without time zone DEFAULT now() NOT NULL,
    "Status" character varying(50) NOT NULL,
    "CurrencyId" character varying(10) NOT NULL
);
    DROP TABLE public."Supply";
       public         heap r       postgres    false            �            1259    16547 
   SupplyItem    TABLE     �   CREATE TABLE public."SupplyItem" (
    "SupplyId" integer NOT NULL,
    "ItemId" integer NOT NULL,
    "Quantity" numeric(18,6) NOT NULL,
    "Price" numeric(18,6) NOT NULL,
    "ReceivedQuantity" numeric(18,6) DEFAULT 0 NOT NULL
);
     DROP TABLE public."SupplyItem";
       public         heap r       postgres    false            �            1259    16529    Supply_SupplyId_seq    SEQUENCE     �   CREATE SEQUENCE public."Supply_SupplyId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE public."Supply_SupplyId_seq";
       public               postgres    false    240            �           0    0    Supply_SupplyId_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public."Supply_SupplyId_seq" OWNED BY public."Supply"."SupplyId";
          public               postgres    false    239            �            1259    16458    Type    TABLE     s   CREATE TABLE public."Type" (
    "ItemType" integer NOT NULL,
    "Description" character varying(255) NOT NULL
);
    DROP TABLE public."Type";
       public         heap r       postgres    false            �            1259    16457    Type_ItemType_seq    SEQUENCE     �   CREATE SEQUENCE public."Type_ItemType_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public."Type_ItemType_seq";
       public               postgres    false    231            �           0    0    Type_ItemType_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public."Type_ItemType_seq" OWNED BY public."Type"."ItemType";
          public               postgres    false    230            �            1259    16451    Unit    TABLE     h   CREATE TABLE public."Unit" (
    "UmId" integer NOT NULL,
    "Name" character varying(255) NOT NULL
);
    DROP TABLE public."Unit";
       public         heap r       postgres    false            �            1259    16450    Unit_UmId_seq    SEQUENCE     �   CREATE SEQUENCE public."Unit_UmId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public."Unit_UmId_seq";
       public               postgres    false    229            �           0    0    Unit_UmId_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public."Unit_UmId_seq" OWNED BY public."Unit"."UmId";
          public               postgres    false    228            �            1259    16397    User    TABLE       CREATE TABLE public."User" (
    "UserId" integer NOT NULL,
    "Name" character varying(255) NOT NULL,
    "Surname" character varying(255) NOT NULL,
    "Email" character varying(255) NOT NULL,
    "Password" character varying(255) NOT NULL,
    "RoleId" integer NOT NULL
);
    DROP TABLE public."User";
       public         heap r       postgres    false            �            1259    16396    User_UserId_seq    SEQUENCE     �   CREATE SEQUENCE public."User_UserId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public."User_UserId_seq";
       public               postgres    false    220            �           0    0    User_UserId_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public."User_UserId_seq" OWNED BY public."User"."UserId";
          public               postgres    false    219            �           2604    16416    Address AddressId    DEFAULT     |   ALTER TABLE ONLY public."Address" ALTER COLUMN "AddressId" SET DEFAULT nextval('public."Address_AddressId_seq"'::regclass);
 D   ALTER TABLE public."Address" ALTER COLUMN "AddressId" DROP DEFAULT;
       public               postgres    false    221    222    222            �           2604    16567 	   Bom BomId    DEFAULT     l   ALTER TABLE ONLY public."Bom" ALTER COLUMN "BomId" SET DEFAULT nextval('public."Bom_BomId_seq"'::regclass);
 <   ALTER TABLE public."Bom" ALTER COLUMN "BomId" DROP DEFAULT;
       public               postgres    false    242    243    243            �           2604    16425    Client ClientId    DEFAULT     x   ALTER TABLE ONLY public."Client" ALTER COLUMN "ClientId" SET DEFAULT nextval('public."Client_ClientId_seq"'::regclass);
 B   ALTER TABLE public."Client" ALTER COLUMN "ClientId" DROP DEFAULT;
       public               postgres    false    223    224    224            �           2604    16487    Inventory InventoryId    DEFAULT     �   ALTER TABLE ONLY public."Inventory" ALTER COLUMN "InventoryId" SET DEFAULT nextval('public."Inventory_InventoryId_seq"'::regclass);
 H   ALTER TABLE public."Inventory" ALTER COLUMN "InventoryId" DROP DEFAULT;
       public               postgres    false    235    234    235            �           2604    16468    Item ItemId    DEFAULT     p   ALTER TABLE ONLY public."Item" ALTER COLUMN "ItemId" SET DEFAULT nextval('public."Item_ItemId_seq"'::regclass);
 >   ALTER TABLE public."Item" ALTER COLUMN "ItemId" DROP DEFAULT;
       public               postgres    false    232    233    233            �           2604    16499    Order OrderId    DEFAULT     t   ALTER TABLE ONLY public."Order" ALTER COLUMN "OrderId" SET DEFAULT nextval('public."Order_OrderId_seq"'::regclass);
 @   ALTER TABLE public."Order" ALTER COLUMN "OrderId" DROP DEFAULT;
       public               postgres    false    237    236    237            �           2604    16393    Role RoleId    DEFAULT     p   ALTER TABLE ONLY public."Role" ALTER COLUMN "RoleId" SET DEFAULT nextval('public."Role_RoleId_seq"'::regclass);
 >   ALTER TABLE public."Role" ALTER COLUMN "RoleId" DROP DEFAULT;
       public               postgres    false    217    218    218            �           2604    16437    Supplier SupplierId    DEFAULT     �   ALTER TABLE ONLY public."Supplier" ALTER COLUMN "SupplierId" SET DEFAULT nextval('public."Supplier_SupplierId_seq"'::regclass);
 F   ALTER TABLE public."Supplier" ALTER COLUMN "SupplierId" DROP DEFAULT;
       public               postgres    false    226    225    226            �           2604    16533    Supply SupplyId    DEFAULT     x   ALTER TABLE ONLY public."Supply" ALTER COLUMN "SupplyId" SET DEFAULT nextval('public."Supply_SupplyId_seq"'::regclass);
 B   ALTER TABLE public."Supply" ALTER COLUMN "SupplyId" DROP DEFAULT;
       public               postgres    false    239    240    240            �           2604    16461    Type ItemType    DEFAULT     t   ALTER TABLE ONLY public."Type" ALTER COLUMN "ItemType" SET DEFAULT nextval('public."Type_ItemType_seq"'::regclass);
 @   ALTER TABLE public."Type" ALTER COLUMN "ItemType" DROP DEFAULT;
       public               postgres    false    231    230    231            �           2604    16454 	   Unit UmId    DEFAULT     l   ALTER TABLE ONLY public."Unit" ALTER COLUMN "UmId" SET DEFAULT nextval('public."Unit_UmId_seq"'::regclass);
 <   ALTER TABLE public."Unit" ALTER COLUMN "UmId" DROP DEFAULT;
       public               postgres    false    228    229    229            �           2604    16400    User UserId    DEFAULT     p   ALTER TABLE ONLY public."User" ALTER COLUMN "UserId" SET DEFAULT nextval('public."User_UserId_seq"'::regclass);
 >   ALTER TABLE public."User" ALTER COLUMN "UserId" DROP DEFAULT;
       public               postgres    false    220    219    220            �          0    16413    Address 
   TABLE DATA           [   COPY public."Address" ("AddressId", "Country", "PostalCode", "City", "Street") FROM stdin;
    public               postgres    false    222   `�       �          0    16564    Bom 
   TABLE DATA           0   COPY public."Bom" ("BomId", "Name") FROM stdin;
    public               postgres    false    243   ��       �          0    16570    BomItem 
   TABLE DATA           B   COPY public."BomItem" ("BomId", "ItemId", "Quantity") FROM stdin;
    public               postgres    false    244   +�       �          0    16422    Client 
   TABLE DATA           C   COPY public."Client" ("ClientId", "Name", "AddressId") FROM stdin;
    public               postgres    false    224   b�       �          0    16445    Currency 
   TABLE DATA           T   COPY public."Currency" ("CurrencyId", "Name", "Symbol", "ExchangeRate") FROM stdin;
    public               postgres    false    227   ŋ       �          0    16484 	   Inventory 
   TABLE DATA           J   COPY public."Inventory" ("InventoryId", "ItemId", "Quantity") FROM stdin;
    public               postgres    false    235   ,�       �          0    16465    Item 
   TABLE DATA           U   COPY public."Item" ("ItemId", "Name", "Description", "UmId", "ItemType") FROM stdin;
    public               postgres    false    233   }�       �          0    16496    Order 
   TABLE DATA           X   COPY public."Order" ("OrderId", "ClientId", "Date", "Status", "CurrencyId") FROM stdin;
    public               postgres    false    237   �       �          0    16513 	   OrderItem 
   TABLE DATA           e   COPY public."OrderItem" ("OrderId", "ItemId", "Quantity", "Price", "DispatchedQuantity") FROM stdin;
    public               postgres    false    238   ��       �          0    16390    Role 
   TABLE DATA           2   COPY public."Role" ("RoleId", "Name") FROM stdin;
    public               postgres    false    218   �       �          0    16434    Supplier 
   TABLE DATA           G   COPY public."Supplier" ("SupplierId", "Name", "AddressId") FROM stdin;
    public               postgres    false    226   D�       �          0    16530    Supply 
   TABLE DATA           \   COPY public."Supply" ("SupplyId", "SupplierId", "Date", "Status", "CurrencyId") FROM stdin;
    public               postgres    false    240   �       �          0    16547 
   SupplyItem 
   TABLE DATA           e   COPY public."SupplyItem" ("SupplyId", "ItemId", "Quantity", "Price", "ReceivedQuantity") FROM stdin;
    public               postgres    false    241   T�       �          0    16458    Type 
   TABLE DATA           ;   COPY public."Type" ("ItemType", "Description") FROM stdin;
    public               postgres    false    231   ��       �          0    16451    Unit 
   TABLE DATA           0   COPY public."Unit" ("UmId", "Name") FROM stdin;
    public               postgres    false    229   ��       �          0    16397    User 
   TABLE DATA           \   COPY public."User" ("UserId", "Name", "Surname", "Email", "Password", "RoleId") FROM stdin;
    public               postgres    false    220   �       �           0    0    Address_AddressId_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public."Address_AddressId_seq"', 1, false);
          public               postgres    false    221            �           0    0    Bom_BomId_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public."Bom_BomId_seq"', 2, true);
          public               postgres    false    242            �           0    0    Client_ClientId_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."Client_ClientId_seq"', 1, false);
          public               postgres    false    223            �           0    0    Inventory_InventoryId_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public."Inventory_InventoryId_seq"', 1, false);
          public               postgres    false    234            �           0    0    Item_ItemId_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."Item_ItemId_seq"', 1, false);
          public               postgres    false    232            �           0    0    Order_OrderId_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public."Order_OrderId_seq"', 1, false);
          public               postgres    false    236            �           0    0    Role_RoleId_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."Role_RoleId_seq"', 1, false);
          public               postgres    false    217            �           0    0    Supplier_SupplierId_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public."Supplier_SupplierId_seq"', 1, false);
          public               postgres    false    225            �           0    0    Supply_SupplyId_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."Supply_SupplyId_seq"', 1, false);
          public               postgres    false    239            �           0    0    Type_ItemType_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public."Type_ItemType_seq"', 1, false);
          public               postgres    false    230            �           0    0    Unit_UmId_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public."Unit_UmId_seq"', 1, false);
          public               postgres    false    228            �           0    0    User_UserId_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."User_UserId_seq"', 1, false);
          public               postgres    false    219            �           2606    16420    Address Address_pkey 
   CONSTRAINT     _   ALTER TABLE ONLY public."Address"
    ADD CONSTRAINT "Address_pkey" PRIMARY KEY ("AddressId");
 B   ALTER TABLE ONLY public."Address" DROP CONSTRAINT "Address_pkey";
       public                 postgres    false    222            �           2606    16574    BomItem BomItem_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public."BomItem"
    ADD CONSTRAINT "BomItem_pkey" PRIMARY KEY ("BomId", "ItemId");
 B   ALTER TABLE ONLY public."BomItem" DROP CONSTRAINT "BomItem_pkey";
       public                 postgres    false    244    244            �           2606    16569    Bom Bom_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public."Bom"
    ADD CONSTRAINT "Bom_pkey" PRIMARY KEY ("BomId");
 :   ALTER TABLE ONLY public."Bom" DROP CONSTRAINT "Bom_pkey";
       public                 postgres    false    243            �           2606    16427    Client Client_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public."Client"
    ADD CONSTRAINT "Client_pkey" PRIMARY KEY ("ClientId");
 @   ALTER TABLE ONLY public."Client" DROP CONSTRAINT "Client_pkey";
       public                 postgres    false    224            �           2606    16449    Currency Currency_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."Currency"
    ADD CONSTRAINT "Currency_pkey" PRIMARY KEY ("CurrencyId");
 D   ALTER TABLE ONLY public."Currency" DROP CONSTRAINT "Currency_pkey";
       public                 postgres    false    227            �           2606    16489    Inventory Inventory_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public."Inventory"
    ADD CONSTRAINT "Inventory_pkey" PRIMARY KEY ("InventoryId");
 F   ALTER TABLE ONLY public."Inventory" DROP CONSTRAINT "Inventory_pkey";
       public                 postgres    false    235            �           2606    16472    Item Item_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public."Item"
    ADD CONSTRAINT "Item_pkey" PRIMARY KEY ("ItemId");
 <   ALTER TABLE ONLY public."Item" DROP CONSTRAINT "Item_pkey";
       public                 postgres    false    233            �           2606    16518    OrderItem OrderItem_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public."OrderItem"
    ADD CONSTRAINT "OrderItem_pkey" PRIMARY KEY ("OrderId", "ItemId");
 F   ALTER TABLE ONLY public."OrderItem" DROP CONSTRAINT "OrderItem_pkey";
       public                 postgres    false    238    238            �           2606    16502    Order Order_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public."Order"
    ADD CONSTRAINT "Order_pkey" PRIMARY KEY ("OrderId");
 >   ALTER TABLE ONLY public."Order" DROP CONSTRAINT "Order_pkey";
       public                 postgres    false    237            �           2606    16395    Role Role_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public."Role"
    ADD CONSTRAINT "Role_pkey" PRIMARY KEY ("RoleId");
 <   ALTER TABLE ONLY public."Role" DROP CONSTRAINT "Role_pkey";
       public                 postgres    false    218            �           2606    16439    Supplier Supplier_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."Supplier"
    ADD CONSTRAINT "Supplier_pkey" PRIMARY KEY ("SupplierId");
 D   ALTER TABLE ONLY public."Supplier" DROP CONSTRAINT "Supplier_pkey";
       public                 postgres    false    226            �           2606    16552    SupplyItem SupplyItem_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public."SupplyItem"
    ADD CONSTRAINT "SupplyItem_pkey" PRIMARY KEY ("SupplyId", "ItemId");
 H   ALTER TABLE ONLY public."SupplyItem" DROP CONSTRAINT "SupplyItem_pkey";
       public                 postgres    false    241    241            �           2606    16536    Supply Supply_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public."Supply"
    ADD CONSTRAINT "Supply_pkey" PRIMARY KEY ("SupplyId");
 @   ALTER TABLE ONLY public."Supply" DROP CONSTRAINT "Supply_pkey";
       public                 postgres    false    240            �           2606    16463    Type Type_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Type"
    ADD CONSTRAINT "Type_pkey" PRIMARY KEY ("ItemType");
 <   ALTER TABLE ONLY public."Type" DROP CONSTRAINT "Type_pkey";
       public                 postgres    false    231            �           2606    16456    Unit Unit_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public."Unit"
    ADD CONSTRAINT "Unit_pkey" PRIMARY KEY ("UmId");
 <   ALTER TABLE ONLY public."Unit" DROP CONSTRAINT "Unit_pkey";
       public                 postgres    false    229            �           2606    16406    User User_Email_key 
   CONSTRAINT     U   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_Email_key" UNIQUE ("Email");
 A   ALTER TABLE ONLY public."User" DROP CONSTRAINT "User_Email_key";
       public                 postgres    false    220            �           2606    16404    User User_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY ("UserId");
 <   ALTER TABLE ONLY public."User" DROP CONSTRAINT "User_pkey";
       public                 postgres    false    220                       2606    16575    BomItem BomItem_BomId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."BomItem"
    ADD CONSTRAINT "BomItem_BomId_fkey" FOREIGN KEY ("BomId") REFERENCES public."Bom"("BomId");
 H   ALTER TABLE ONLY public."BomItem" DROP CONSTRAINT "BomItem_BomId_fkey";
       public               postgres    false    244    4860    243                       2606    16580    BomItem BomItem_ItemId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."BomItem"
    ADD CONSTRAINT "BomItem_ItemId_fkey" FOREIGN KEY ("ItemId") REFERENCES public."Item"("ItemId");
 I   ALTER TABLE ONLY public."BomItem" DROP CONSTRAINT "BomItem_ItemId_fkey";
       public               postgres    false    244    233    4848                        2606    16428    Client Client_AddressId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Client"
    ADD CONSTRAINT "Client_AddressId_fkey" FOREIGN KEY ("AddressId") REFERENCES public."Address"("AddressId");
 J   ALTER TABLE ONLY public."Client" DROP CONSTRAINT "Client_AddressId_fkey";
       public               postgres    false    224    4836    222                       2606    16490    Inventory Inventory_ItemId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Inventory"
    ADD CONSTRAINT "Inventory_ItemId_fkey" FOREIGN KEY ("ItemId") REFERENCES public."Item"("ItemId");
 M   ALTER TABLE ONLY public."Inventory" DROP CONSTRAINT "Inventory_ItemId_fkey";
       public               postgres    false    235    233    4848                       2606    16478    Item Item_ItemType_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Item"
    ADD CONSTRAINT "Item_ItemType_fkey" FOREIGN KEY ("ItemType") REFERENCES public."Type"("ItemType") ON DELETE RESTRICT;
 E   ALTER TABLE ONLY public."Item" DROP CONSTRAINT "Item_ItemType_fkey";
       public               postgres    false    231    233    4846                       2606    16473    Item Item_UmId_fkey    FK CONSTRAINT     z   ALTER TABLE ONLY public."Item"
    ADD CONSTRAINT "Item_UmId_fkey" FOREIGN KEY ("UmId") REFERENCES public."Unit"("UmId");
 A   ALTER TABLE ONLY public."Item" DROP CONSTRAINT "Item_UmId_fkey";
       public               postgres    false    233    229    4844                       2606    16524    OrderItem OrderItem_ItemId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."OrderItem"
    ADD CONSTRAINT "OrderItem_ItemId_fkey" FOREIGN KEY ("ItemId") REFERENCES public."Item"("ItemId");
 M   ALTER TABLE ONLY public."OrderItem" DROP CONSTRAINT "OrderItem_ItemId_fkey";
       public               postgres    false    4848    233    238                       2606    16519     OrderItem OrderItem_OrderId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."OrderItem"
    ADD CONSTRAINT "OrderItem_OrderId_fkey" FOREIGN KEY ("OrderId") REFERENCES public."Order"("OrderId");
 N   ALTER TABLE ONLY public."OrderItem" DROP CONSTRAINT "OrderItem_OrderId_fkey";
       public               postgres    false    238    4852    237                       2606    16503    Order Order_ClientId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Order"
    ADD CONSTRAINT "Order_ClientId_fkey" FOREIGN KEY ("ClientId") REFERENCES public."Client"("ClientId");
 G   ALTER TABLE ONLY public."Order" DROP CONSTRAINT "Order_ClientId_fkey";
       public               postgres    false    224    237    4838                       2606    16508    Order Order_CurrencyId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Order"
    ADD CONSTRAINT "Order_CurrencyId_fkey" FOREIGN KEY ("CurrencyId") REFERENCES public."Currency"("CurrencyId");
 I   ALTER TABLE ONLY public."Order" DROP CONSTRAINT "Order_CurrencyId_fkey";
       public               postgres    false    237    227    4842                       2606    16440     Supplier Supplier_AddressId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Supplier"
    ADD CONSTRAINT "Supplier_AddressId_fkey" FOREIGN KEY ("AddressId") REFERENCES public."Address"("AddressId");
 N   ALTER TABLE ONLY public."Supplier" DROP CONSTRAINT "Supplier_AddressId_fkey";
       public               postgres    false    4836    226    222                       2606    16558 !   SupplyItem SupplyItem_ItemId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."SupplyItem"
    ADD CONSTRAINT "SupplyItem_ItemId_fkey" FOREIGN KEY ("ItemId") REFERENCES public."Item"("ItemId");
 O   ALTER TABLE ONLY public."SupplyItem" DROP CONSTRAINT "SupplyItem_ItemId_fkey";
       public               postgres    false    233    241    4848                       2606    16553 #   SupplyItem SupplyItem_SupplyId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."SupplyItem"
    ADD CONSTRAINT "SupplyItem_SupplyId_fkey" FOREIGN KEY ("SupplyId") REFERENCES public."Supply"("SupplyId");
 Q   ALTER TABLE ONLY public."SupplyItem" DROP CONSTRAINT "SupplyItem_SupplyId_fkey";
       public               postgres    false    241    4856    240            	           2606    16542    Supply Supply_CurrencyId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Supply"
    ADD CONSTRAINT "Supply_CurrencyId_fkey" FOREIGN KEY ("CurrencyId") REFERENCES public."Currency"("CurrencyId");
 K   ALTER TABLE ONLY public."Supply" DROP CONSTRAINT "Supply_CurrencyId_fkey";
       public               postgres    false    240    227    4842            
           2606    16537    Supply Supply_SupplierId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Supply"
    ADD CONSTRAINT "Supply_SupplierId_fkey" FOREIGN KEY ("SupplierId") REFERENCES public."Supplier"("SupplierId");
 K   ALTER TABLE ONLY public."Supply" DROP CONSTRAINT "Supply_SupplierId_fkey";
       public               postgres    false    4840    240    226            �           2606    16407    User User_RoleId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_RoleId_fkey" FOREIGN KEY ("RoleId") REFERENCES public."Role"("RoleId");
 C   ALTER TABLE ONLY public."User" DROP CONSTRAINT "User_RoleId_fkey";
       public               postgres    false    220    218    4830            �   �   x�3���)�N�40�500�O,*�J,O��3�6e����`J�AJ�8���o.�t9�T���`�eS`R`��X�_�����tvrfiqUv����	L�)H�	gxQ~�Ѧ�rΣ��3S�2����s��qqq ��2�      �       x�3��I,(�/�2�)J�.�/����� X=�      �   '   x�3�4�4�3 .#N3NCǜ����4���b���� 
 �      �   S   x�3�IM���Q.�S�R�����4�2�t*M�M,Q�s��4�2�tL/�*+�NDVi�e��XZ�2D��$E�ӄ+F��� +�5      �   W   x������)��T�:ڔ_R�Yu���P� �\C�8]K��95��4�3����p���$)8�Uf'm��TaQ���� G�      �   A   x�E��	�0�����b�1�X{{0�a��&�cz�L�E��<��XXo6��3D��$~      �   �  x�]�Kn�0���)��%�i���+F�"]t3�[�c��B.����Ght��]$K�����_�/�G��Z?�5���\sZ5����BGM��?�(EYT����#9�ц�ma���ե�F#ɬ���X��Q}����1*{d��zڧ�����w��5�ݮ�
m�4�ƛHC���CT��^2�*.�g
Q#l�����S9i�G�&�u�9=5��pZ�:�TV�k�Ѡk�n�Z�-�H���5MϲyJn��F�&Tŕ���#y�D;��*���N|%Ʌ��Γq���h�Q&���J��Ӟ �E��QAu1�����/���_Z������mhLrwl㽸5l{J�󙨓�*&�Y�^�uzXM��A1g�ϋ�(�_���      �   o   x�3�4�4202�50�5�T04�26�2��32�436��O�J�.�:Қ�����e�i�Oy�BAQUez~I~yb^f)�kh�1�1>-QE��9�U +L8M�uQh�W� B�0%      �   M   x�3�4�4�3 N c�\��Ɯ�paC$%0}\F�& 	��B�cN3N#�S$c`�\&����`qJ� Z��      �   F   x�3��M�KL9�'��ˈ3 '1/��$�˘�;?��4�$1�D!#1/%'���˄�71=��2/�:F��� �E      �   �   x�E�A
�0@�u�C ��V7��.��`Ck�	���x�����3��Aid�}�:�|�ў�5E��`m)��������@hK�*�f�ߩh���G��y�����%̡0u�&�����x?��n­,�7O*�y� | ��8G      �   Z   x�3�4�4202�50�5�T04�26�2��32�436��O�J�.�:Қ����e�i�O�K~qIbQrU~^*g���1�1��]C��b���� � "�      �   @   x�E��	  ��0�Z����ϡ6yGG ���~�#��jƎ��F�$��"��n$�]J      �   ,   x�3�(�O)�.Qp?�txsy^%��w~nA~^j^	W� �bX      �   (   x�3��*)�N�2�����O/J��2���,)����� ���      �   �   x���=
�@�뷇ȏ��6*b!�2�����L`��Sx��������I�f�M�\k%���js{g�Ԗ���d-mqnX5��@bR,�g��u����Vj2�%t-��yy�qx�Eѱ�<����X���6�s��L�iR����X؊��!բ�c��j�     
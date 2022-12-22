
$(document).ready(function () {

    var arrLang = {

        'tr': {

            '0': 'Ad',
            '1': 'Soyad',
            '2': 'Kullanıcı adı',
            '3': 'Telefon',
            '4': 'Şifre',
            '5': 'Cüzdan',
            '6': 'Oluştur',
            '7': 'Role',
            '8': 'Gir',
            '9': 'Hesap Oluştur',
            '10': 'Satın al',
            '11': "Sat",
            '12': "Güç",
            '13': 'Yaş',
            '14': 'Tür',
            '15': 'Fiyat',
            '16': 'Yorum at',
            '17': 'Kaldır',
            '18': 'Güncelle',
            '19': 'Yeni Pokemon Ekle',
            '20': 'Pokemon Oluştur',
            '21': 'Pokemon Güncelle',
            '22': 'Kullanıcı getir',
            '23': 'Pokemonları',
            '24': 'Çıkış Yap',
            '25': 'Markette',
            '26': 'Satıcı',
        },


        'en': {
            '0': 'Name',
            '1': 'Surname',
            '2': 'Username',
            '3': 'Phone',
            '4': 'Password',
            '5': 'Wallet',
            '6': 'Create',
            '7': 'Role',
            '8': 'Login',
            '9': 'Create Account',
            '10': 'Buy',
            '11': 'Sell',
            '12': 'Power',
            '13': 'Age',
            '14': 'Species',
            '15': 'Price',
            '16': 'Add a Note',
            '17': 'Remove',
            '18': 'Update',
            '19': 'Add New Pokemon',
            '20': 'Create Pokemon',
            '21': 'Update Pokemon',
            '22': 'Get User',
            '23': "'s Pokemons",
            '24': 'Log Out',
            '25': 'At Market',
            '26': 'Seller',
        },


    };



    $('.dropdown-item').click(function () {
        localStorage.setItem('dil', JSON.stringify($(this).attr('id')));
        location.reload();
    });

    var lang = JSON.parse(localStorage.getItem('dil'));

    if (lang == "en") {
        $("#drop_yazı").html("English");
    }
    else {
        $("#drop_yazı").html("Türkçe");

    }

    $('a,h5,p,h1,h2,span,li,button,h3,label').each(function (index, element) {
        $(this).text(arrLang[lang][$(this).attr('key')]);

    });

});
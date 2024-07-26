let menus;
let loaders

$(() => {
    menus = buildMenus();
    loaders = buildLoaders();
});

let buildMenus = () => {
    menus = new MenuStorage();

    $('.menu').each((idx, item) => {
        let menu = $(item);
        let head = $($(menu).children()[0]);
        let list = $($(menu).children()[1]);

        switch (menu.data('component')) {
            case 'menu':
                menus.toStorage(new NSMenu(idx, menu, menus, head, list)
                    .includeNodes());
                break;
            case 'menu-single':
                menus.toStorage(new SSMenu(idx, menu, menus, head, list, menu.data('rel-text'), menu.data('rel-secret'))
                    .includeNodes());
                break;
            case 'menu-multiple':
                menus.toStorage(new MSMenu(idx, menu, menus, head, list, menu.data('rel-text'), menu.data('rel-secret'))
                    .includeNodes());
                break;
            default: return menus;
        }
    });

    return menus;
}

let buildLoaders = () => {
    loaders = [];

    $('input[type="file"]').each((idx, item) => {
        let loader = $(item);

        loaders.push(new Loader(loader, $(loader.data('dest'))));
    });

    return loaders;
}
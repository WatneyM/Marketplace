let menus;
let loaders;
let collapsables;

$(() => {
    menus = buildMenus();
    loaders = buildLoaders();
    collapsables = buildCollapsables();

    disableScrollOnMenuKeyAction();
});

let disableScrollOnMenuKeyAction = () => {
    window.addEventListener('keydown', (ev) => {
        if ($(ev.target).hasClass('menu-node') && ev.key === 'ArrowDown') {
            ev.preventDefault();
        }
    });
    window.addEventListener('keyup', (ev) => {
        if ($(ev.target).hasClass('menu-node') && ev.key === 'ArrowUp') {
            ev.preventDefault();
        }
    });
}

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
            default: return menus;
        }
    });

    return menus;
}

let buildLoaders = () => {
    loaders = [];

    $('input[type="file"]').each((idx, item) => {
        let loader = $(item);

        loaders.push(new Loader(loader, $(loader.data('dest')), $(loader.data('img')), $(loader.data('label'))));
    });

    return loaders;
}

let buildCollapsables = () => {
    collapsables = new CollapsableStorage();

    $('*[data-component="collapsable"]').each((idx, item) => {
        collapsables.toStorage(new Collapsable(idx, $(item), collapsables));
    });

    return collapsables;
}
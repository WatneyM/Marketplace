class MenuStorage {
    #activeIdx;

    constructor() {
        this.storage = [];
        this.#activeIdx = 0;

        this.fromStorage = (idx) => { return this.storage[idx]; }
        this.toStorage = (menu) => { this.storage.push(menu); }

        this.activate = (idx) => {
            if (this.#activeIdx != idx) {
                this.fromStorage(this.#activeIdx).popout();

                this.#activeIdx = idx;
            }
        }
    }
}

class NSMenu {
    #idx;

    #active;
    #currentFocus;

    constructor(idx, root, storage, headerTag, listTag) {
        this.#idx = idx;
        this.root = root;
        this.storage = storage;
        this.head = headerTag;
        this.list = listTag;

        this.nodes = [];
        this.#active = false;

        this.head.on('click', () => { this.clickEvent(); })
            .on('focusout', (ev) => { this.focusOutEvent(ev) });
    }

    clickEvent() {
        if (this.#active) {
            this.popout();
        } else {
            this.popup();
        }

        this.storage.activate(this.#idx);
    }
    focusOutEvent(ev) {
        if ($(ev.relatedTarget).parents('.menu').length === 0
            || !$(ev.relatedTarget).hasClass('menu-node')) {
            this.popout();
        }
    }

    popup() {
        this.list.addClass('d-block');
        setTimeout(() => {
            this.list.addClass('menu-popup');
            this.#active = true;
        }, 100);

        this.setInitialFocus();
    }
    popout() {
        this.list.removeClass('menu-popup');
        setTimeout(() => {
            this.list.removeClass('d-block');
            this.#active = false;
        }, this.milliseconds(this.list.css('transition-duration')));
    }

    setInitialFocus() {
        this.#currentFocus = 0;

        while (this.nodes[this.#currentFocus].node.prop('disabled')) this.#currentFocus++;

        this.nodes[this.#currentFocus].focusOnNode();
    }
    preFocusNext(idx) {
        if (idx != this.nodes.length - 1) {
            this.focusNext();
        } else return;
    }
    preFocusPrev(idx) {
        if (idx != 0) {
            this.focusPrev();
        } else return;
    }
    focusNext() {
        do {
            if (this.#currentFocus + 1 === this.nodes.length) {
                this.#currentFocus--;
                return;
            } else this.#currentFocus++;
        } while (this.nodes[this.#currentFocus].node.prop('disabled'));

        this.nodes[this.#currentFocus].focusOnNode();
    }
    focusPrev() {
        do {
            if (this.#currentFocus - 1 === -1) {
                this.#currentFocus++;
                return;
            } else this.#currentFocus--;
        } while (this.nodes[this.#currentFocus].node.prop('disabled'));

        this.nodes[this.#currentFocus].focusOnNode();
    }

    milliseconds(duration) {
        return parseFloat(duration) * (/\ds$/.test(duration) ? 1000 : 1);
    }

    includeNodes() {
        let idxC = 0;

        this.list.children().each((idx, item) => {
            let node = $(item);
            switch (node.data('node')) {
                case 'ns':
                    this.nodes.push(new NSMenuNode(idxC, node, this));
                    idxC++;
                    break;
                default: return this;
            }
        });

        return this;
    }
}
class SSMenu extends NSMenu {
    #target;
    #secret;

    #activeNodeIdx;

    constructor(idx, root, storage, headerTag, listTag, targetSelector, secretSelector) {
        super(idx, root, storage, headerTag, listTag);

        this.#target = targetSelector;
        this.#secret = secretSelector;

        this.#activeNodeIdx = -1;

        this.swapSelectedNode = (idx) => {
            if (this.#activeNodeIdx === -1 || this.#activeNodeIdx === idx) {
                this.#activeNodeIdx = idx;

                return;
            }

            this.nodes[this.#activeNodeIdx].unselectNode();

            this.#activeNodeIdx = idx;
        }
    }

    unselectAll() {
        let temp = this.nodes.filter(rule => $(rule.node).data('node') === 'ss');

        this.clear();
        for (var node of temp) {
            node.unselectNode();
        }
    }

    clear() {
        $(this.#target).val(undefined);
        $(this.#secret).val(undefined);
    }

    includeNodes() {
        this.list.children().each((idx, item) => {
            let node = $(item);
            switch (node.data('node')) {
                case 'ss':
                    this.nodes.push(new SSMenuNode(idx, this, node, this.#secret,
                        this.#target, node.text(), node.data('val')));
                    break;
                case 'cs':
                    this.nodes.push(new CSMenuNode(idx, node, this));
                default: return this;
            }
        });

        return this;
    }
}

class NSMenuNode {
    #idx;
    #menu;

    constructor(idx, nodeTag, menuTag) {
        this.#idx = idx;
        this.node = nodeTag;
        this.#menu = menuTag;

        this.node.on('focusout', (ev) => {
            if (ev.originalEvent === undefined || ev.originalEvent.relatedTarget === null) {
                this.#menu.focusOutEvent(ev);
                this.#menu.head.trigger('focus');

                return;
            }
            if ($(ev.originalEvent.relatedTarget).parentsUntil(this.#menu.root).length === 0) {
                return;
            }
            if ($(ev.originalEvent.relatedTarget).parentsUntil(this.#menu.root).length !== 0) {
                this.#menu.focusOutEvent(ev);
            }
        }).on('keydown', (ev) => { this.keyDownEvent(ev); });
    }

    keyDownEvent(ev) {
        switch (ev.which) {
            case 27:
                this.node.trigger('focusout');
                break;
            case 38:
                this.#menu.preFocusPrev(this.#idx);
                break;
            case 40:
                this.#menu.preFocusNext(this.#idx);
                break;
        }
    }

    focusOnNode() {
        this.node.trigger('focus');
    }
}
class SSMenuNode extends NSMenuNode {
    #idx;
    #menu;

    #secret;
    #target;
    #nodeData;
    #secretData;

    constructor(idx, menuTag, nodeTag, secretTag, targetTag, nodeData, secretData) {
        super(idx, nodeTag, menuTag);

        this.#idx = idx;
        this.#menu = menuTag;
        this.#secret = secretTag;
        this.#target = targetTag;
        this.#nodeData = nodeData;
        this.#secretData = secretData;

        this.node.on('click', () => { this.clickEvent(); });

        if (this.node.prop('disabled')) {
            this.node.trigger('click');
        }
    }

    clickEvent() {
        this.selectNode();
        this.node.trigger('focusout');
    }

    selectNode() {
        $(this.#target).val(this.#nodeData);
        $(this.#secret).val(this.#secretData);

        this.node.prop('disabled', true);

        this.#menu.swapSelectedNode(this.#idx);
    }
    unselectNode() {
        this.node.prop('disabled', false);
    }
}

class CSMenuNode extends NSMenuNode {
    #menu;

    constructor(idx, nodeTag, menuTag) {
        super(idx, nodeTag, menuTag);

        this.#menu = menuTag;

        this.node.on('click', () => { this.clickEvent(); });
    }

    clickEvent() {
        this.#menu.unselectAll();
        this.node.trigger('focusout');
    }
}
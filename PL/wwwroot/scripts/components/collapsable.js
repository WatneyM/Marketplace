class CollapsableStorage {
    #storage;
    #activeIdx;

    constructor() {
        this.#storage = [];
        this.#activeIdx = 0;

        this.fromStorage = (idx) => { return this.#storage[idx]; }
        this.toStorage = (clps) => { this.#storage.push(clps); }

        this.activate = (idx) => {
            if (this.#activeIdx != idx) {
                this.fromStorage(this.#activeIdx).popout();

                this.#activeIdx = idx;
            }
        }
    }
}

class Collapsable {
    #idx;
    #component;
    #storage;

    constructor(idx, component, storage) {
        this.#idx = idx;
        this.#component = component;
        this.#storage = storage;

        this.content = $(this.#component.children('.collapsable-content'));
        this.arrow = $(this.#component.children('.collapsable-down'));

        $(this.#component).on('click', () => {
            this.clickEvent();

            this.#storage.activate(this.#idx);
        });
    }

    clickEvent() {
        if (this.content.css('max-height') !== '0px') {
            this.popout();
        }
        else {
            this.popup();
        }
    }

    popup() {
        this.content.removeClass('d-none');
        this.content.css('max-height', this.content[0].scrollHeight + 'px');

        this.arrow.addClass('collapsable-up');
    }
    popout() {
        this.content.css('max-height', '0px');
        this.arrow.removeClass('collapsable-up');

        setTimeout(() => {
            this.content.addClass('d-none');
        }, this.milliseconds(this.content.css('transition-duration')));
    }

    milliseconds(duration) {
        return parseFloat(duration) * (/\ds$/.test(duration) ? 1000 : 1);
    }
}
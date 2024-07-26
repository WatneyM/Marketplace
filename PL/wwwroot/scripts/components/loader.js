
class Loader {
    #self;
    #dest;

    constructor(loaderTag, destTag) {
        this.#self = loaderTag;
        this.#dest = destTag;

        this.#self.on('change', (ev) => {
            let reader = new FileReader();

            reader.readAsDataURL(ev.target.files[0]);

            $(reader).on('load', () => {
                this.#dest.val(reader.result);
            });
        });
    }
}
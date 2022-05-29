export class AsyncQueue<R> {
  private readonly queue: {
    action: (...args: unknown[]) => Promise<R>;
    resolve: (value: R) => void;
    reject: (reason: unknown) => void;
  }[] = [];
  private pendingPromise: boolean;

  constructor() {
    this.pendingPromise = false;
  }

  enqueue = (action: (...args: unknown[]) => Promise<R>): Promise<R> => {
    return new Promise<R>((resolve, reject) => {
      this.queue.push({ action, resolve, reject });
      void this.dequeue();
    });
  };

  dequeue = async () => {
    if (this.pendingPromise) return false;

    const item = this.queue.shift();

    if (!item) return false;

    try {
      this.pendingPromise = true;
      const payload = await item.action(this);
      item.resolve(payload);
      this.pendingPromise = false;
    } catch (e) {
      item.reject(e);
      this.pendingPromise = false;
    } finally {
      void this.dequeue();
    }
  };
}

export {};

declare global {
  interface Window {
    google?: {
      accounts: {
        id: {
          initialize: (params: {
            client_id: string;
            callback: (response: { credential: string }) => void;
            auto_select?: boolean;
          }) => void;
          renderButton: (parent: HTMLElement, options: unknown) => void;
          prompt: () => void;
          disableAutoSelect: () => void;
        };
      };
    };
  }
}

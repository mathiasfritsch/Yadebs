describe('Account list tests', () => {

  beforeEach(() => {
    cy.intercept('GET', '/api/Accounts', { fixture: 'accounts.json' }).as('accounts');

  });

  it('shows list off accounts', () => {
    cy.visit('/accounts/list');
    cy.get('[data-cy="account-entry"]').should('have.length', 3);
  })
})

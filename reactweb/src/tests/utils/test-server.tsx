import { rest } from 'msw';
import { setupServer } from 'msw/node';

const houses = [
    {
        id: "house-1",
        address: "123 test st",
        country: "usa",
        description: "house #1",
        price: 1000
    },
    {
        id: "house-2",
        address: "234 main st",
        country: "usa",
        description: "house #2",
        price: 2000
    }
];

export const handlers = [
    rest.get('*/houses', (req, res, ctx) => {
        return res(
            ctx.status(200),
            ctx.json(houses)
        )
    }),
    rest.get('*/houses/house-1', (req, res, ctx) => {
        return res(
            ctx.status(200),
            ctx.json(houses[0])
        )
    })
];

export const server = setupServer(...handlers);

// Establish API mocking before all tests.
beforeAll(() => server.listen())
// Reset any request handlers that we may add during the tests,
// so they don't affect other tests.
afterEach(() => server.resetHandlers())
// Clean up after the tests are finished.
afterAll(() => server.close())